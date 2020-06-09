export class PaginationModel {
    constructor(public currentPage: number = 1,
                public lastPage: number = null,
                public recordPerPage: number = 30,
                public recordPerPageName: string = '30',
                public total: number = null,
                public list: any = [],
                public from: number = 1,
                public to: number = 1) {
    }
}
