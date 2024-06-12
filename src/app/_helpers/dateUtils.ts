import { formatDate } from '@angular/common';

export class DateUtils {
    static getFormattedDate(date: string) {
        if (!date || (typeof (date) == 'string' && date.trim().length === 0)) return '';

        return formatDate(date, 'MM-dd-yyy', 'en')
    }
}