interface IFieldValuePair {
    field: string;
    value: string;
}

export interface IDataParameter {
    filters: IFieldValuePair[];
    sortKey: string;
    sortDirection: number;
    offset: number;
    limit: number;

    [key: string]: any;
}