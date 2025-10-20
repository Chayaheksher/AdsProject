import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-graph-step',
  templateUrl: './graph-step.component.html',
  styleUrls: ['./graph-step.component.css']
})
export class GraphStepComponent {
  @Input() step:stepGraph;
  
}

export interface stepGraph{
  userName:string;
  dateAndTime:Date;
  ishurType:string;
  approvalOrRejectionCode:number;
}