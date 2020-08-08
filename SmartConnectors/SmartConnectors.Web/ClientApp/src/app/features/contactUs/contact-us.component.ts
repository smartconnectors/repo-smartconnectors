import { Component, EventEmitter, OnInit, OnDestroy } from '@angular/core';
import { NgClass, NgIf } from '@angular/common';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { HttpService } from '~/core/http';

@Component({ selector: 'app-contact-us-component', templateUrl: './contact-us.component.html', styleUrls: ['./contact-us.component.scss'] })

export class ContactUsComponent implements OnInit, OnDestroy {
    private isFormSubmitted: boolean;
    private contactUsForm: FormGroup;
    private showErrorMessage: boolean;
    subscription: Subscription;

    public constructor(private fb: FormBuilder, private emailService: HttpService, ) {
        this.contactUsForm = this.fb.group({
            contactName: new FormControl('', Validators.required),
            companyName: new FormControl('', Validators.required),
            email: new FormControl('', Validators.required),
            phone: new FormControl('', Validators.required),
            message: new FormControl('', Validators.required),
            type: new FormControl('contact-us')
        });

        this.subscription = new Subscription();
    }

    public ngOnInit(): void {
        this.isFormSubmitted = false;
        this.showErrorMessage = false;
    }

    public onSubmit() {

    }

    private onSuccess(res) {
        this.contactUsForm.reset();
        this.isFormSubmitted = true;
    }

    private onError(err) {
        this.showErrorMessage = true;
    }

    public ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    get contactName() { return this.contactUsForm.get('contactName'); }
    get companyName() { return this.contactUsForm.get('companyName'); }
    get email() { return this.contactUsForm.get('email'); }
    get phone() { return this.contactUsForm.get('phone'); }
    get message() { return this.contactUsForm.get('message'); }
}
