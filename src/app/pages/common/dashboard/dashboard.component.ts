import { Component, OnInit } from '@angular/core';
import { AppPageBaseComponent } from 'src/app/_components/base/app-page-base.component';
import { NotificationMessages } from 'src/app/_enums/notification-messages';
import { ProductService } from 'src/app/demo/service/product.service';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { DashboardService } from 'src/app/services/dashboard.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
    templateUrl: './dashboard.component.html',
})
export class DashboardComponent extends AppPageBaseComponent implements OnInit {
    barData: any;
    barOptions: any;
    barDataLabels: string[] = [];
    barDataMembersDataSets: any[] = [];
    barDataGuestsDataSets: any[] = [];

    constructor(
        private dashboardService: DashboardService,
        private productService: ProductService,
        public layoutService: LayoutService,
        public override notifService: NotificationService) {
        super(notifService);
    }

    ngOnInit() {
        this.getAttendanceData();
        this.setBreadcrumbs([
            { label: 'Home' },
            { label: 'Dashboard', url: '' }
        ]);
    }

    private getAttendanceData() {
        this.dashboardService.getAttendanceData().subscribe({
            next: (data: any) => {
                // console.log('data', data);

                if (data && data?.datasets && data?.datasets.length > 0) {
                    data?.datasets.forEach(data => {
                        const label = data['dateTimeLabel'];
                        const guestsCount = data['guestsCount'];
                        const membersCount = data['membersCount'];

                        this.barDataLabels.push(label);
                        this.barDataMembersDataSets.push(membersCount);
                        this.barDataGuestsDataSets.push(guestsCount);
                    });
                }

                this.initChart();
            },
            error: (e) => {
                this.handleErrorMessage(e, NotificationMessages.GenericError.Message);
            }
        });
    }

    initChart() {
        const documentStyle = getComputedStyle(document.documentElement);
        const textColor = documentStyle.getPropertyValue('--text-color');
        const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
        const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

        this.barData = {
            labels: this.barDataLabels,
            datasets: [
                {
                    label: 'Members',
                    backgroundColor: documentStyle.getPropertyValue('--primary-500'),
                    borderColor: documentStyle.getPropertyValue('--primary-500'),
                    data: this.barDataMembersDataSets
                },
                {
                    label: 'Guests',
                    backgroundColor: documentStyle.getPropertyValue('--primary-200'),
                    borderColor: documentStyle.getPropertyValue('--primary-200'),
                    data: this.barDataGuestsDataSets
                }
            ]
        };

        this.barOptions = {
            plugins: {
                legend: {
                    labels: {
                        fontColor: textColor
                    }
                }
            },
            scales: {
                x: {
                    ticks: {
                        color: textColorSecondary,
                        font: {
                            weight: 500
                        }
                    },
                    grid: {
                        display: false,
                        drawBorder: false
                    }
                },
                y: {
                    ticks: {
                        color: textColorSecondary
                    },
                    grid: {
                        color: surfaceBorder,
                        drawBorder: false
                    }
                },
            }
        };
    }
}
