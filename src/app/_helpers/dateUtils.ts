import { formatDate } from '@angular/common';

export class DateUtils {
    static getFormattedDate(date: string) {
        if (!date || date.trim().length === 0) return '';

        return formatDate(date, 'MM-dd-yyy', 'en')
    }
}