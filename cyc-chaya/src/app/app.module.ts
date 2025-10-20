import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import{HttpClientModule}from '@angular/common/http';
import { AppComponent } from './app.component';
import { HomePageComponent } from './home-page/home-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ManagerComponent } from './manager/manager.component';
import { UsersComponent } from './users/users.component';
import { EditOrNewComponent } from './edit-or-new/edit-or-new.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModuleModule } from './material-module/material-module.module';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import {Component} from '@angular/core';
import {MatTableModule} from '@angular/material/table';
import { AdsComponent } from './ads/ads.component';
import { GraphsComponent } from './graphs/graphs.component';
import { userGouardGuard } from './user-gouard.guard';
import { ManagerTableGouardGuard } from './manager-table.guard';
import {MatDialog, MatDialogRef, MatDialogModule} from '@angular/material/dialog';
import {MatSelectModule} from '@angular/material/select';
import {MatRadioModule} from '@angular/material/radio';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatCardModule} from '@angular/material/card'
import {MatTabsModule} from '@angular/material/tabs';
import { StatusComponent } from './status/status.component';
import { AdDiaryComponent } from './ad-diary/ad-diary.component';
import { AdHistoryComponent } from './ad-history/ad-history.component';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import {NgFor} from '@angular/common';
import {MatListModule} from '@angular/material/list';
import {MatTooltipModule} from '@angular/material/tooltip';
import { NgChartsModule } from 'ng2-charts';
import { MatNativeDateModule } from '@angular/material/core';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { TestChartComponent } from './test-chart/test-chart.component';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { GraphStepComponent } from './graph-step/graph-step.component';
@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    ManagerComponent,
    UsersComponent,
    EditOrNewComponent,
    AdsComponent,
    GraphsComponent,
    StatusComponent,
    AdDiaryComponent,
    AdHistoryComponent,
    TestChartComponent,
    GraphStepComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      {path:'home' ,component: HomePageComponent },
      // {path:'manager', component: ManagerComponent},
      {path:'manager',canActivate:[userGouardGuard], component: ManagerComponent, children:([{path:'users',component:UsersComponent},
        {path:'ads',component:AdsComponent},{path:'graphs',component:GraphsComponent}, {path:'**', component:UsersComponent,canActivate:[ManagerTableGouardGuard]}
      ])},
      // {path:'user', component: UsersComponent},
      {path:'editNew', component:EditOrNewComponent},
      {path:'ads', component:AdsComponent, children:([{path:'status', component:StatusComponent},
        {path:'adDiary/:adId', component:AdDiaryComponent},
      {path:'adHistory', component:AdHistoryComponent}]
      )},
      {path:'',redirectTo: 'home', pathMatch: 'full' },
      {path:'**', redirectTo:'home', pathMatch:'full'}
    ]),

    BrowserAnimationsModule,
    HttpClientModule,
    MatSortModule,
    MatPaginatorModule,
    MatInputModule, MatButtonModule, MatIconModule,
    MatTableModule,
    MatDialogModule,
    FormsModule, MatFormFieldModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatCardModule, MatCheckboxModule, FormsModule, MatRadioModule,
    MatTabsModule,
    NgFor,
    MatListModule,
    MatTooltipModule,
    NgChartsModule,
    MatDatepickerModule, MatNativeDateModule,
    MatSortModule,
    MatAutocompleteModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
