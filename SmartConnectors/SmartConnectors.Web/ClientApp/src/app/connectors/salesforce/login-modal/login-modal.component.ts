import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '~/core/services/authentication.service';
import { AlertService } from '~/core/services';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SalesforceService } from '../salesforce.service';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'sf-login-modal',
  templateUrl: './login-modal.component.html',
  styleUrls: ['./login-modal.component.scss']
})

export class SalesforceLoginModalComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  errorMsg: string;

  constructor(
    private formBuilder: FormBuilder,
    private salesforceService: SalesforceService,
    private alertService: AlertService,
    public dialogRef: MatDialogRef<SalesforceLoginModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {

  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      endpointName: ['services/Soap/u/29.0/'],
      serverHost: [environment.saleforceServerHostUrl],
      username: ['', Validators.required],
      password: ['', Validators.required],
      securityToken: ['x6FXoVqItJUgrBJEXUcP0NAg', Validators.required]
    });


  }

  onEnableSandbox(event) {
    if (event.checked) {
      this.loginForm.get("serverHost").setValue(environment.saleforceSandboxedServerHostUrl)
    } else {
      this.loginForm.get("serverHost").setValue(environment.saleforceServerHostUrl)
    }
  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.salesforceService.login(this.loginForm.value, this.data.workflowId, this.data.connectorId)
      .subscribe(
        data => {
          if (data && !data.success) {
            this.errorMsg = data.message;
          } else {
            this.dialogRef.close(data);
          }
          this.loading = false;
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }
}
