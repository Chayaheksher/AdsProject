import { AfterViewInit, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { AdsService } from '../ads.service';
import Chart, { elements } from 'chart.js/auto';
import { stepGraph } from '../graph-step/graph-step.component';
import { interval, Subscription } from 'rxjs';

@Component({
  selector: 'app-ad-history',
  templateUrl: './ad-history.component.html',
  styleUrls: ['./ad-history.component.css']
})
export class AdHistoryComponent implements OnInit, OnDestroy {
  constructor(private _serviceAds:AdsService){}
  dataSource:stepGraph[];
  chart: any ;
  @Input() adId:any;
  ngOnInit(): void {
    let step:stepGraph
      const sub1 =  this._serviceAds.AdDiary(this.adId).subscribe(
        (next:any)=>{
          this.dataSource = next.map(x=>({ishurType: x.ishurType, userName: x.userName, 
            approvalOrRejectionCode :x.approvalOrRejectionCode, dateAndTime: x.dateAndTime}));
        const userName = next.map(element => element.userName);
        const dateAndTime = next.map(element => element.dateAndTime);
        const approvalOrRejection = next.map(element => element.approvalOrRejection);

// Creating the chart
this.chart = new Chart('MyChart5', {
  type: 'line',  
  data: {
    labels: dateAndTime, 
    datasets: [{
      label: 'כמות אישורים', 
      data: approvalOrRejection,  
      borderColor: 'blue',  
      backgroundColor: 'rgba(0, 0, 255, 0.2)', 
      fill: true,  
      tension: 0.1, 
      pointRadius: 3,  
      pointBackgroundColor: 'blue',
    }]
  },
  options: {
    responsive: true, 
    plugins: {
      legend: {
        display: true, 
      },
      tooltip: {
        callbacks: {
          label: function(context) {
            return `${context.label}: ${context.raw}`;
          }
        }
      }
    },
    scales: {
      x: {
        title: {
          display: true,
          text: 'תאריך ושעה', 
        },
        type: 'time',
        time: {
          unit: 'day', 
          tooltipFormat: 'll HH:mm' 
        },
      },
      y: {
        title: {
          display: true,
          text: 'סבב אישורים',
        },
        beginAtZero: true,  
        ticks: {
          stepSize: 1 
        }
      }
    }
  }
});
},
        (err: any) => {
          console.error("שגיאה בשליפת נתונים", err); 
        }
      )
      this.subs.push(sub1);
    }
    private subs: Subscription[] = [];
    ngOnDestroy(): void {
      this.subs.forEach((s) => s.unsubscribe());
    }
  }