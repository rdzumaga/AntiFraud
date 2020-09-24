import { Component, OnInit } from '@angular/core';
import { PurchaseClient, PurchaseDto } from '../api.client';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  constructor(private purchaseClient: PurchaseClient) { }

  purchases$: Observable<PurchaseDto[]>;

  ngOnInit() {
    this.purchases$ = this.purchaseClient.getPurchases();
  }

}
