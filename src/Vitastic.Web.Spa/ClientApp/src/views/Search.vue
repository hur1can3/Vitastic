<template>
  <b-container>
    <h1 class="mt-4">
      Search Recipes
    </h1>
    <EntityTableControls
      :clear-search="clearSearch"
      :init-search="startSearch"
      class="mt-4"
    >
      <b-form-row
        slot="searchControls"
      >
        <b-col
          sm="12"
          md="6"
        >
          <b-input-group
            prepend="Name contains"
            class="mb-2"
          >
            <b-form-input
              id="nameSearch"
              v-model="workingRequest.name"
              name="nameSearch"
            />
          </b-input-group>
        </b-col>
        <b-col
          sm="12"
          md="6"
        >
          <b-input-group
            prepend="Categories contain"
            class="mb-2"
          >
            <b-form-input
              id="categorySearch"
              v-model="workingRequest.category"
              name="categorySearch"
            />
          </b-input-group>
        </b-col>
      </b-form-row>
    </EntityTableControls>
    <b-table
      :items="listResponse.items"
      :fields="tableFields"
      :sort-by="workingRequest.sortBy"
      :sort-desc="workingRequest.sortDesc"
      sort-icon-left
      no-local-sorting
      show-empty
      hover
      class="mt-4"
      @row-clicked="showDetails"
      @sort-changed="tableSortChanged"
    >
      <template #cell(name)="data">
        <router-link
          class="table-link"
          :to="{ name: 'view', params: { id: data.item.id } }"
        >
          {{ data.value }}
        </router-link>
      </template>
    </b-table>
    <EntityTablePager
      :list-response="listResponse"
      :list-request="listRequest"
      :change-page="changePage"
      :change-take="changeTake"
      class="mt-4"
    />
  </b-container>
</template>

<script>
import { mapActions, mapGetters } from 'vuex';
import ListRecipesRequest from '@/models/api/recipes/ListRecipesRequest';
import { numberOrDefault } from '@/models/formatters';
import webApi from '@/webApi';
import EntityTableControls from '@/viewComponents/EntityTableControls.vue';
import EntityTablePager from '@/viewComponents/EntityTablePager.vue';

export default {
  components: {
    EntityTableControls,
    EntityTablePager,
  },
  props: {
    query: {
      type: Object,
      required: false,
      default: () => {},
    },
  },
  data() {
    return {
      workingRequest: new ListRecipesRequest(),
    };
  },
  computed: {
    ...mapGetters({
      listResponse: 'recipes/listResponse',
      listRequest: 'recipes/listRequest',
    }),
    tableFields() {
      return [
        {
          key: 'name',
          sortable: true,
        },
        {
          key: 'categories',
          formatter: (value) => value.join(', '),
        },
      ];
    },
  },
  watch: {
    workingRequest: {
      handler(request) {
        this.setListRequest({ ...request });
      },
      deep: true,
    },
  },
  created() {
    if (Object.keys(this.query).length !== 0) {
      const defaultRequest = new ListRecipesRequest();

      this.workingRequest = {
        ...defaultRequest,
        ...this.query,
        sortDesc: JSON.parse(this.query.sortDesc) === true,
        page: numberOrDefault(this.query.page, defaultRequest.page),
        take: numberOrDefault(this.query.take, defaultRequest.take),
      };
    } else {
      this.workingRequest = this.listRequest;
    }
    this.fetchList();
  },
  methods: {
    ...mapActions({
      setApiFailureMessages: 'app/setApiFailureMessages',
      setListResponse: 'recipes/setListResponse',
      setListRequest: 'recipes/setListRequest',
    }),
    fetchList() {
      this.$router.replace({ query: this.workingRequest }).catch(() => {});

      webApi.recipes.list(
        this.workingRequest,
        (data) => this.setListResponse(data),
        (response) => this.setApiFailureMessages(response),
      );
    },
    clearSearch() {
      this.workingRequest = {
        ...new ListRecipesRequest(),
        take: this.workingRequest.take,
      };
      this.fetchList();
    },
    startSearch() {
      this.workingRequest.page = 1;
      this.fetchList();
    },
    changePage(page) {
      this.workingRequest.page = page;
      this.fetchList();
    },
    changeTake(take) {
      this.workingRequest.isPagingEnabled = take !== null;
      this.workingRequest.take = take;
      this.workingRequest.page = 1;
      this.fetchList();
    },
    showDetails(recipe) {
      this.$router.push({ name: 'view', params: { id: recipe.id } });
    },
    tableSortChanged(table) {
      this.workingRequest = {
        ...this.workingRequest,
        sortBy: table.sortBy,
        sortDesc: table.sortDesc,
      };
      this.fetchList();
    },
  },
};
</script>

<style lang="scss" scoped>
@import "@/style/theme";

.table-link {
  color: $body-color;
}
</style>
