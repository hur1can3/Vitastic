<template>
  <div
    id="app"
    @keydown.stop.prevent.esc="clearMessages()"
  >
    <vue-progress-bar />
    <AppHeader
      :brand="applicationName"
      :user="user"
    >
      <AppNav slot="navItems" />
    </AppHeader>
    <AppMessageCenter
      class="no-print"
    />
    <main>
      <router-view />
    </main>
    <AppFooter />
  </div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex';
import initializeStore from '@/models/initializeStore';
import AppMessageCenter from '@/viewComponents/AppMessageCenter.vue';
import AppHeader from '@/viewComponents/AppHeader.vue';
import AppNav from '@/viewComponents/AppNav.vue';
import AppFooter from '@/viewComponents/AppFooter.vue';

export default {
  components: {
    AppMessageCenter,
    AppHeader,
    AppNav,
    AppFooter,
  },
  computed: {
    ...mapGetters({
      applicationName: 'app/applicationName',
      user: 'app/user',
    }),
  },
  watch: {
    applicationName(newApplicationName) {
      document.title = newApplicationName;
    },
  },
  mounted() {
    initializeStore();
  },
  methods: {
    ...mapActions({
      clearMessages: 'app/clearMessages',
    }),
  },
};
</script>

<style lang="scss">
@import "@/style/theme";

// Sticky footer
html,
body,
#app {
  height: 100%;
}
#app {
  display: flex;
  flex-direction: column;
}
main {
  flex: 1 0 auto;
}
footer {
  flex-shrink: 0;
}

// Modal background
.modal-content {
  background: $body-bg;
}

// Minimum button width
Button animation input[type="button"].btn,
input[type="submit"].btn,
button.btn,
a.btn,
.btn {
  min-width: 5rem;
}

// Colored headings
h1,
h2,
h3,
h4,
h5,
h6,
.h1,
.h2,
.h3,
.h4,
.h5,
.h6 {
  color: $primary;
}

// Hover table cursor
table.table-hover {
  cursor: pointer;
}

// Printable screens
@media screen {
  .print-only {
    display: none;
  }
}

@media print {
  .no-print {
    display: none;
  }

  div {
    background-color: $white;
  }

  button,
  .btn {
    display: none;
  }

  p,
  pre,
  h1,
  h2,
  h3,
  h4 {
    color: $black;
  }
}
</style>
