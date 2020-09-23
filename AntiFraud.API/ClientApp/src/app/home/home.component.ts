import { Component } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators, FormArray } from '@angular/forms';
import { PurchaseClient } from '../api.client';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  validated = false;
  error = false;

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

  constructor(private fb: FormBuilder, private purchaseClient: PurchaseClient, private router: Router) { }

  onSubmit() {
    this.error = false;
    this.validated = true;
    if (!this.purchaseForm.valid) return;

    this.purchaseClient.createPurchase(this.purchaseForm.value)
      .subscribe({
        next: (result) => {
          const id = result.id;
          this.router.navigate(['/view', id]);
        },
        error: (_) => {
          this.error = true;
        }
      });
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
