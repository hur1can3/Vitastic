//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.18.0.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v10.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

namespace FastEndpoints {

export class ApiClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        this.http = http ? http : window as any;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    /**
     * @return Success
     */
    vitasticDomainEndpointsRecipesDeleteRecipe(id: number): Promise<VitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32> {
        let url_ = this.baseUrl + "/api/recipes/{Id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{Id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "DELETE",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processVitasticDomainEndpointsRecipesDeleteRecipe(_response);
        });
    }

    protected processVitasticDomainEndpointsRecipesDeleteRecipe(response: Response): Promise<VitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = VitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32.fromJS(resultData200);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<VitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32>(null as any);
    }

    /**
     * @param nameSearch (optional) 
     * @param categorySearch (optional) 
     * @param sortBy (optional) 
     * @return Success
     */
    vitasticDomainEndpointsRecipesListRecipes(nameSearch: string | undefined, categorySearch: string | undefined, sortBy: string | undefined, sortDesc: boolean, isPagingEnabled: boolean, page: number, take: number): Promise<VitasticCoreSharedKernalResponsesCollectionsIItemSetOfListRecipesResponse> {
        let url_ = this.baseUrl + "/api/recipes?";
        if (nameSearch === null)
            throw new Error("The parameter 'nameSearch' cannot be null.");
        else if (nameSearch !== undefined)
            url_ += "NameSearch=" + encodeURIComponent("" + nameSearch) + "&";
        if (categorySearch === null)
            throw new Error("The parameter 'categorySearch' cannot be null.");
        else if (categorySearch !== undefined)
            url_ += "CategorySearch=" + encodeURIComponent("" + categorySearch) + "&";
        if (sortBy === null)
            throw new Error("The parameter 'sortBy' cannot be null.");
        else if (sortBy !== undefined)
            url_ += "SortBy=" + encodeURIComponent("" + sortBy) + "&";
        if (sortDesc === undefined || sortDesc === null)
            throw new Error("The parameter 'sortDesc' must be defined and cannot be null.");
        else
            url_ += "SortDesc=" + encodeURIComponent("" + sortDesc) + "&";
        if (isPagingEnabled === undefined || isPagingEnabled === null)
            throw new Error("The parameter 'isPagingEnabled' must be defined and cannot be null.");
        else
            url_ += "IsPagingEnabled=" + encodeURIComponent("" + isPagingEnabled) + "&";
        if (page === undefined || page === null)
            throw new Error("The parameter 'page' must be defined and cannot be null.");
        else
            url_ += "Page=" + encodeURIComponent("" + page) + "&";
        if (take === undefined || take === null)
            throw new Error("The parameter 'take' must be defined and cannot be null.");
        else
            url_ += "Take=" + encodeURIComponent("" + take) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processVitasticDomainEndpointsRecipesListRecipes(_response);
        });
    }

    protected processVitasticDomainEndpointsRecipesListRecipes(response: Response): Promise<VitasticCoreSharedKernalResponsesCollectionsIItemSetOfListRecipesResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = VitasticCoreSharedKernalResponsesCollectionsIItemSetOfListRecipesResponse.fromJS(resultData200);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<VitasticCoreSharedKernalResponsesCollectionsIItemSetOfListRecipesResponse>(null as any);
    }

    /**
     * @return Success
     */
    vitasticDomainEndpointsRecipesSaveRecipe(saveRecipeRequest: VitasticDomainEndpointsRecipesSaveRecipeRequest): Promise<VitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32> {
        let url_ = this.baseUrl + "/api/recipes";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(saveRecipeRequest);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processVitasticDomainEndpointsRecipesSaveRecipe(_response);
        });
    }

    protected processVitasticDomainEndpointsRecipesSaveRecipe(response: Response): Promise<VitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = VitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32.fromJS(resultData200);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<VitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32>(null as any);
    }
}

export class ApiClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        this.http = http ? http : window as any;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    error(): Promise<FileResponse> {
        let url_ = this.baseUrl + "/error";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/octet-stream"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processError(_response);
        });
    }

    protected processError(response: Response): Promise<FileResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200 || status === 206) {
            const contentDisposition = response.headers ? response.headers.get("content-disposition") : undefined;
            let fileNameMatch = contentDisposition ? /filename\*=(?:(\\?['"])(.*?)\1|(?:[^\s]+'.*?')?([^;\n]*))/g.exec(contentDisposition) : undefined;
            let fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[3] || fileNameMatch[2] : undefined;
            if (fileName) {
                fileName = decodeURIComponent(fileName);
            } else {
                fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
                fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
            }
            return response.blob().then(blob => { return { fileName: fileName, data: blob, status: status, headers: _headers }; });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<FileResponse>(null as any);
    }

    forbidden(): Promise<FileResponse> {
        let url_ = this.baseUrl + "/forbidden";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/octet-stream"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processForbidden(_response);
        });
    }

    protected processForbidden(response: Response): Promise<FileResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200 || status === 206) {
            const contentDisposition = response.headers ? response.headers.get("content-disposition") : undefined;
            let fileNameMatch = contentDisposition ? /filename\*=(?:(\\?['"])(.*?)\1|(?:[^\s]+'.*?')?([^;\n]*))/g.exec(contentDisposition) : undefined;
            let fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[3] || fileNameMatch[2] : undefined;
            if (fileName) {
                fileName = decodeURIComponent(fileName);
            } else {
                fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
                fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
            }
            return response.blob().then(blob => { return { fileName: fileName, data: blob, status: status, headers: _headers }; });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<FileResponse>(null as any);
    }
}

export class VitasticCoreSharedKernalResponsesMessagesUserMessage implements IVitasticCoreSharedKernalResponsesMessagesUserMessage {
    message?: string;

    constructor(data?: IVitasticCoreSharedKernalResponsesMessagesUserMessage) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.message = _data["message"];
        }
    }

    static fromJS(data: any): VitasticCoreSharedKernalResponsesMessagesUserMessage {
        data = typeof data === 'object' ? data : {};
        let result = new VitasticCoreSharedKernalResponsesMessagesUserMessage();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["message"] = this.message;
        return data;
    }
}

export interface IVitasticCoreSharedKernalResponsesMessagesUserMessage {
    message?: string;
}

export class VitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32 extends VitasticCoreSharedKernalResponsesMessagesUserMessage implements IVitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32 {
    id?: number;
    uriString?: string | undefined;

    constructor(data?: IVitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.id = _data["id"];
            this.uriString = _data["uriString"];
        }
    }

    static fromJS(data: any): VitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32 {
        data = typeof data === 'object' ? data : {};
        let result = new VitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["uriString"] = this.uriString;
        super.toJSON(data);
        return data;
    }
}

export interface IVitasticCoreSharedKernalResponsesMessagesEntityMessageOfInt32 extends IVitasticCoreSharedKernalResponsesMessagesUserMessage {
    id?: number;
    uriString?: string | undefined;
}

export class VitasticDomainEndpointsRecipesDeleteRecipeRequest implements IVitasticDomainEndpointsRecipesDeleteRecipeRequest {

    constructor(data?: IVitasticDomainEndpointsRecipesDeleteRecipeRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
    }

    static fromJS(data: any): VitasticDomainEndpointsRecipesDeleteRecipeRequest {
        data = typeof data === 'object' ? data : {};
        let result = new VitasticDomainEndpointsRecipesDeleteRecipeRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        return data;
    }
}

export interface IVitasticDomainEndpointsRecipesDeleteRecipeRequest {
}

export abstract class VitasticCoreSharedKernalResponsesCollectionsIItemSetOfListRecipesResponse implements IVitasticCoreSharedKernalResponsesCollectionsIItemSetOfListRecipesResponse {
    count?: number;
    items?: VitasticDomainEndpointsRecipesListRecipesResponse[];
    isPagingEnabled?: boolean;
    page?: number;
    take?: number;
    totalCount?: number;

    constructor(data?: IVitasticCoreSharedKernalResponsesCollectionsIItemSetOfListRecipesResponse) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.count = _data["count"];
            if (Array.isArray(_data["items"])) {
                this.items = [] as any;
                for (let item of _data["items"])
                    this.items!.push(VitasticDomainEndpointsRecipesListRecipesResponse.fromJS(item));
            }
            this.isPagingEnabled = _data["isPagingEnabled"];
            this.page = _data["page"];
            this.take = _data["take"];
            this.totalCount = _data["totalCount"];
        }
    }

    static fromJS(data: any): VitasticCoreSharedKernalResponsesCollectionsIItemSetOfListRecipesResponse {
        data = typeof data === 'object' ? data : {};
        throw new Error("The abstract class 'VitasticCoreSharedKernalResponsesCollectionsIItemSetOfListRecipesResponse' cannot be instantiated.");
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["count"] = this.count;
        if (Array.isArray(this.items)) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        data["isPagingEnabled"] = this.isPagingEnabled;
        data["page"] = this.page;
        data["take"] = this.take;
        data["totalCount"] = this.totalCount;
        return data;
    }
}

export interface IVitasticCoreSharedKernalResponsesCollectionsIItemSetOfListRecipesResponse {
    count?: number;
    items?: VitasticDomainEndpointsRecipesListRecipesResponse[];
    isPagingEnabled?: boolean;
    page?: number;
    take?: number;
    totalCount?: number;
}

export class VitasticDomainEndpointsRecipesListRecipesResponse implements IVitasticDomainEndpointsRecipesListRecipesResponse {
    id?: number;
    name?: string | undefined;
    categories?: string[] | undefined;
    imageId?: number | undefined;

    constructor(data?: IVitasticDomainEndpointsRecipesListRecipesResponse) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            if (Array.isArray(_data["categories"])) {
                this.categories = [] as any;
                for (let item of _data["categories"])
                    this.categories!.push(item);
            }
            this.imageId = _data["imageId"];
        }
    }

    static fromJS(data: any): VitasticDomainEndpointsRecipesListRecipesResponse {
        data = typeof data === 'object' ? data : {};
        let result = new VitasticDomainEndpointsRecipesListRecipesResponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        if (Array.isArray(this.categories)) {
            data["categories"] = [];
            for (let item of this.categories)
                data["categories"].push(item);
        }
        data["imageId"] = this.imageId;
        return data;
    }
}

export interface IVitasticDomainEndpointsRecipesListRecipesResponse {
    id?: number;
    name?: string | undefined;
    categories?: string[] | undefined;
    imageId?: number | undefined;
}

export class VitasticDomainEndpointsRecipesListRecipesRequest implements IVitasticDomainEndpointsRecipesListRecipesRequest {

    constructor(data?: IVitasticDomainEndpointsRecipesListRecipesRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
    }

    static fromJS(data: any): VitasticDomainEndpointsRecipesListRecipesRequest {
        data = typeof data === 'object' ? data : {};
        let result = new VitasticDomainEndpointsRecipesListRecipesRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        return data;
    }
}

export interface IVitasticDomainEndpointsRecipesListRecipesRequest {
}

export class VitasticDomainEndpointsRecipesSaveRecipeRequest implements IVitasticDomainEndpointsRecipesSaveRecipeRequest {
    id?: number;
    name!: string;
    ingredients!: string;
    directions!: string;
    cookTimeMinutes?: number | undefined;
    prepTimeMinutes?: number | undefined;
    categories?: string[] | undefined;

    constructor(data?: IVitasticDomainEndpointsRecipesSaveRecipeRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.ingredients = _data["ingredients"];
            this.directions = _data["directions"];
            this.cookTimeMinutes = _data["cookTimeMinutes"];
            this.prepTimeMinutes = _data["prepTimeMinutes"];
            if (Array.isArray(_data["categories"])) {
                this.categories = [] as any;
                for (let item of _data["categories"])
                    this.categories!.push(item);
            }
        }
    }

    static fromJS(data: any): VitasticDomainEndpointsRecipesSaveRecipeRequest {
        data = typeof data === 'object' ? data : {};
        let result = new VitasticDomainEndpointsRecipesSaveRecipeRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["ingredients"] = this.ingredients;
        data["directions"] = this.directions;
        data["cookTimeMinutes"] = this.cookTimeMinutes;
        data["prepTimeMinutes"] = this.prepTimeMinutes;
        if (Array.isArray(this.categories)) {
            data["categories"] = [];
            for (let item of this.categories)
                data["categories"].push(item);
        }
        return data;
    }
}

export interface IVitasticDomainEndpointsRecipesSaveRecipeRequest {
    id?: number;
    name: string;
    ingredients: string;
    directions: string;
    cookTimeMinutes?: number | undefined;
    prepTimeMinutes?: number | undefined;
    categories?: string[] | undefined;
}

export interface FileResponse {
    data: Blob;
    status: number;
    fileName?: string;
    headers?: { [name: string]: any };
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    if (result !== null && result !== undefined)
        throw result;
    else
        throw new ApiException(message, status, response, headers, null);
}

}