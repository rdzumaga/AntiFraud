import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription, Observable } from 'rxjs';
import { PurchaseClient, PurchaseDto } from '../api.client';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.css']
})
export class PurchaseComponent implements OnInit, OnDestroy {

  purchase$: Observable<PurchaseDto>;
  private sub: Subscription;

  constructor(private route: ActivatedRoute, private purchaseClient: PurchaseClient) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.purchase$ = this.purchaseClient.getPurchase(params['id']);
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

}
