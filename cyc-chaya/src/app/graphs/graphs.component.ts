import { Component, OnDestroy, OnInit } from '@angular/core';
import Chart, { ChartConfiguration, ChartType } from 'chart.js/auto';
import { GraphsService } from '../graphs.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-graphs',
  templateUrl: './graphs.component.html',
  styleUrls: ['./graphs.component.css']
})
export class GraphsComponent implements OnInit, OnDestroy {
  selectedMonth: string;
  public chart: (Chart<'bar', any[], any> | Chart<'pie', any[], any> | Chart<'bar', any[], any> | Chart<'bar', any[], any>)[] = [];
  private subs: Subscription[] = [];
   public isChart1:boolean = false;
   public isChart2:boolean = false;
   public isChart3:boolean = false;
   public isChart4:boolean = false;

  constructor(private _graphsService: GraphsService) {
    const today = new Date();
    this.selectedMonth = today.toISOString().slice(0, 7);
  }

  ngOnInit(): void {
    this.loadCharts();
  }

  loadCharts(): void {
    this.destroyCharts();

    // Subscription for ApprovalUsers data
    const sub1 = this._graphsService.ApprovalUsers(this.selectedMonth).subscribe(
      (next: any[]) => {
        const lstUserName = next.map(element => element.userName);
        const lstCountApprovals = next.map(element => element.countIshurim);
    
        this.chart[0] = new Chart<'bar', any[], any>("MyChart", {
          type: 'bar',
          data: {
            labels: lstUserName,
            datasets: [{
              label: "כמות אישורים",
              data: lstCountApprovals,
              backgroundColor: 'red'
            }]
          }
        });
        if(next.length===0) this.isChart1=true
      },
      (err: any) => {
        console.error("Error fetching ApprovalUsers data", err);
      }
    );
    this.subs.push(sub1);

    // Subscription for WhereStatusAds data
    const sub2 = this._graphsService.WhereStatusAds(this.selectedMonth).subscribe(
      (next: any[]) => {
        const adStatusName = next.map(element => element.adStatusName);
        const countAdsInStatus = next.map(element => element.countAdsInStatus);
        const colors = this.generateRandomColors(adStatusName.length);
    
        // Create pie chart
        this.chart[1] = new Chart<'pie', any[], any>("MyChart1", {
          type: 'pie',
          data: {
            labels: adStatusName,
            datasets: [{
              label: "כמות מודעות בסטטוס זה",
              data: countAdsInStatus,
              backgroundColor: colors 
            }]
          }
        });
        if(next.length===0) this.isChart2=true
      },
      (err: any) => {
        console.error("Error fetching WhereStatusAds data", err);
      }
    );
    this.subs.push(sub2);

    // Subscription for AdCategory data
    const sub3 = this._graphsService.AdCategory(this.selectedMonth).subscribe(
      (next: any[]) => {
        const adTypesName = next.map(element => element.adTypesName);
        const countAdsInType = next.map(element => element.countAdsInType);
        const colors = this.generateRandomColors(adTypesName.length);
    
        this.chart[2] = new Chart<'pie', any[], any>("MyChart2", {
          type: 'pie',
          data: {
            labels: adTypesName,
            datasets: [{
              label: "כמות מודעות לסוג מודעות",
              data: countAdsInType,
              backgroundColor: colors 
            }]
          }
        });
        if(next.length===0) this.isChart3=true
      },
      (err: any) => {
        console.error("Error fetching AdCategory data", err);
      }
    );
    this.subs.push(sub3);

    // Subscription for SumCustomerCharge data
    const sub4 = this._graphsService.SumCustomerCharge(this.selectedMonth).subscribe(
      (next: any[]) => {
        const customerName = next.map(element => element.customerName);
        const sumCharge = next.map(element => element.sumCharge);
    
        this.chart[3] = new Chart<'bar', any[], any>("MyChart3", {
          type: 'bar',
          data: {
            labels: customerName,
            datasets: [{
              label: "חוב לקוח",
              data: sumCharge,
              backgroundColor: 'blue',
            }]
          },
          options: {
            indexAxis: 'y',
            scales: {
              x: {
                beginAtZero: true
              },
              y: {
                beginAtZero: true,
              }
            }
          }
        });
        if(next.length===0) this.isChart4=true
      },
      (err: any) => {
        console.error("Error fetching SumCustomerCharge data", err);
      }
    );
    this.subs.push(sub4);
  }

  generateRandomColors(numColors: number): string[] {
    const colors = [];
    for (let i = 0; i < numColors; i++) {
      const color = '#' + Math.floor(Math.random() * 16777215).toString(16);
      colors.push(color);
    }
    return colors;
  }

  destroyCharts(): void {
    this.chart.forEach(chartInstance => {
      if (chartInstance) {
        chartInstance.destroy();
      }
    });
    this.chart = [];
  }

  ngOnDestroy(): void {
    this.subs.forEach((s) => s.unsubscribe());
    this.destroyCharts();
  }

  // Method to handle change in selected month
  ChangeDate(event: any): void {
    this.selectedMonth = event.target.value;
    this.loadCharts();
  }
}
