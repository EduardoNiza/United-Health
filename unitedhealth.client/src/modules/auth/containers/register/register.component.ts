import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { AlertService } from '@common/services';
import { AuthService } from '@modules/auth/services/auth.service';
import { first } from 'rxjs/operators';

@Component({
    selector: 'sb-register',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './register.component.html',
    styleUrls: ['register.component.scss'],
})
export class RegisterComponent implements OnInit {
    form!: FormGroup;
    submitted: boolean = false;
    loading: boolean = false;

    profiles = [
        {name: "Patient", value: 0},
        {name: "Medical", value: 1},
        {name: "Nutrition", value: 2},
        {name: "Training", value: 3},
    ]

    constructor(
        private formBuilder: FormBuilder,
        private authService: AuthService,
        private alertService: AlertService,
        private cdr: ChangeDetectorRef
    ) {}
    ngOnInit() {
        this.form = this.formBuilder.group({
            name: ['', Validators.required],
            username: ['', Validators.required],
            birthDate: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            phoneNumber: ['', [Validators.required, Validators.pattern(/^\d+$/)]],
            type: [null, Validators.required],
            password: ['', Validators.required],
            confirmPassword: ['', Validators.required]
        },
        {
          validators: this.checkPasswords,
        });
    }

    checkPasswords: ValidatorFn = (group: AbstractControl):  ValidationErrors | null => { 
        let pass = group.get('password')?.value;
        let confirmPass = group.get('confirmPassword')?.value
        return pass === confirmPass ? null : { notSame: true }
    }

    get f() { return this.form.controls; }

    onSubmit() {
        this.submitted = true;
        
        // reset alerts on submit
        this.alertService.clear();
        
        // stop here if form is invalid
        if (this.form.invalid) {
            return;
        }

        let formObject = {
            name: this.f.name.value,
            username: this.f.username.value,
            password: this.f.password.value,
            birthDate: this.f.birthDate.value,
            email: this.f.email.value,
            phoneNumber: this.f.phoneNumber.value,
            type: this.f.type.value
        }
        
        this.loading = true;
        this.authService.register(formObject)
            .pipe(first())
            .subscribe({
                next: () => {
                    this.alertService.success("Registration successful!")
                    this.loading = false;
                    this.cdr.detectChanges();
                },
                error: error => {
                    this.alertService.error(error.message);
                    this.loading = false;
                    this.cdr.detectChanges();
                }
            });
    }
}
