interface IFilterObject {
    start: number;
    rows: number;
    sortField: string;
    sortOrder: number;
    filters: IFilterKeyPair[];
}

interface IFilterKeyPair {
    field: string;
    value: string;
}