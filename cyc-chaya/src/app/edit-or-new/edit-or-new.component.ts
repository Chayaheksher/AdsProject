import { Component, EventEmitter, Inject, OnDestroy, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { UsersDetailsService } from '../users-details.service';
import { UsersComponent } from '../users/users.component';
import { ManagerComponent } from '../manager/manager.component';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UsersDetailsInterface } from '../users-details-interface';
import { Router } from '@angular/router';
import { interval, Subscription } from 'rxjs';

export interface Communication {
    userId:number;
    communicationID:number;
  communicationTypeDescription:string;
  communicationDetails: string;
  mainComunication: boolean;
}

@Component({
  selector: 'app-edit-or-new',
  templateUrl: './edit-or-new.component.html',
  styleUrls: ['./edit-or-new.component.css']
})
export class EditOrNewComponent implements OnInit, OnDestroy {

  public roles: string[];
  public communicationString: string[];
  public editOrNewUser: FormGroup;
  public dataSource: UsersDetailsInterface[];
  public getCom: Communication[];
  public nowMainId: number;
  hide = true;

  constructor(
    private _service: UsersDetailsService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.editOrNewUser = this.fb.group({
      userId:[null],
      fullName: ['', Validators.required],
      userName: ['', Validators.required],
      passwords: ['', [Validators.required, Validators.pattern(/[0-9]/)]],
      charactersName: ['', Validators.required],
      getCommunication: this.fb.array([])
    });

    const sub1 =  this._service.GetUsers().subscribe(
      (next: any) => {
        this.dataSource = next.users;
        if (this.data.rowIndex !== -1) {
          this.populateForm();
        }
        else{
          this.addCom()
        }
      },
      (err: any) => {
        console.error(err);
        alert('אירעה שגיאה');
      }
    );
this.subs.push(sub1);
    const sub2 =  this._service.GetRoleName().subscribe(
      (next: any) => {
        this.roles = next;
      },
      (err: any) => {
        console.error(err);
        alert('אירעה שגיאה');
      }
    );
    this.subs.push(sub2);

    const sub3 =  this._service.GetCom().subscribe(
      (next: any) => {
        this.getCom = next;
        if (this.data.rowIndex !== -1) {
          this.fillCom();
        }
      },
      (err: any) => {
        console.error(err);
        alert('אירעה שגיאה');
      }
    );
    this.subs.push(sub3);
    const sub4 =  this._service.GetComName().subscribe(
      (next: any) => {
        this.communicationString = next;
      },
      (err: any) => {
        console.error(err);
        alert('אירעה שגיאה');
      }
    );
    this.subs.push(sub4);
  }

  populateForm(): void {
    const user = this.dataSource[this.data.rowIndex];
    this.editOrNewUser.patchValue({
      userId:user.userId,
      fullName: user.fullName,
      userName: user.userName,
      passwords: user.passwords,
      charactersName: user.charactersName
    });
  }

  fillCom(): void {
    const control = this.editOrNewUser.get('getCommunication') as FormArray;
    this.getCom.forEach(com => {
    
     
      
      if (com.userId === this.dataSource[this.data.rowIndex].userId) {
        control.push(this.fb.group({
          communicationID:[com.communicationID],
          communicationTypeDescription: [com.communicationTypeDescription, Validators.required],
          communicationDetails: [com.communicationDetails, Validators.required],
          mainComunication: [com.mainComunication]
        }));
        if (com.mainComunication) {
          this.nowMainId = com.communicationID;
        }
      }
    });
  }

  addCom(): void {
    const isFirst = this.getCommunication.length === 0;
    const newCommunication = this.fb.group({
      communicationID: [null],
      communicationTypeDescription: ['', Validators.required],
      communicationDetails: ['', Validators.required],
      mainComunication: [isFirst]
    });
    (this.editOrNewUser.get('getCommunication') as FormArray).push(newCommunication);
    if (isFirst) {
      this.nowMainId = null; // Ensure there's no current main communication ID initially
    }
  }
  lowCom(): void {
    const getCommunicationArray = this.editOrNewUser.get('getCommunication') as FormArray;
    
    if (getCommunicationArray.length > 1) {
      getCommunicationArray.removeAt(getCommunicationArray.length - 1);
    }
  }
  setMainCommunication(index: number): void {
    const controlArray = this.editOrNewUser.get('getCommunication') as FormArray;
        controlArray.controls.forEach((control, idx) => {
      const mainControl = control.get('mainComunication');
      if (mainControl) {
        mainControl.setValue(idx === index); 
        if (idx === index) {
          this.nowMainId = this.getCommunication.at(idx).get('communicationID').value;
        }
      }
    });
  }
  
  @Output() refreshRequested = new EventEmitter<void>();

  save(): void {
    if (this.editOrNewUser.valid) {
      const userData = this.editOrNewUser.value;
      const sub5= this._service.SaveUserDetails(userData).subscribe(
        (next: any) => {
          alert(next.str);
          this.refreshRequested.emit();
        },
        (err: any) => {
          console.error(err);
          alert('Failed to save user details.');
        }
      );
      this.subs.push(sub5);
    } else {
      alert('Form is invalid. Please check all fields.');
    }
  }

  get getCommunication(): FormArray {
    return this.editOrNewUser.get('getCommunication') as FormArray;
  }
  private subs: Subscription[] = [];
  ngOnDestroy(): void {
    this.subs.forEach((s) => s.unsubscribe());
  }
}
