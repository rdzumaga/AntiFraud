import { Component } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators, FormArray } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  validated = false;

  purchaseForm = this.fb.group({
    email: ['', [Validators.minLength(1), Validators.email, Validators.required]],
    amount: ['', [Validators.min(0), Validators.required]],
    currency: ['', [Validators.required]],
    address: this.fb.group({
      street: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      zipcode: ['', Validators.required]
    }),
    products: this.fb.array([this.fb.group({
      name: ['', [Validators.required, Validators.min(0)]],
      quantity: ['', [Validators.required]]
    })])
  });

  constructor(private fb: FormBuilder) { }

  onSubmit() {
    this.validated = true;
    if (!this.purchaseForm.valid) return;
  }


  addProduct() {
    const products = this.purchaseForm.controls.products as FormArray;
    products.push(this.fb.group({
      name: ['', [Validators.required, Validators.min(0)]],
      quantity: ['', [Validators.required]]
    }));
  }

  removeProduct(index: number) {
    const products = this.purchaseForm.controls.products as FormArray;
    if (products.length === 1) return;
    products.removeAt(index);
  }

}
