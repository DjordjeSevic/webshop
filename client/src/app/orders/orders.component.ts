import { Component, OnInit } from '@angular/core';
import { Order } from '../shared/models/address';
import { OrdersService } from './orders.service';
import { AccountService } from '../account/account.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit{
  orders: Order[] = [];

  constructor(private orderService: OrdersService, private accountService: AccountService) { }
  
  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe(
      user => {
        if (user?.role === "Admin") {
          this.getAllOrders()
        } else {
          this.getOrders();
        }
    })
  }
  
  getOrders() {
    this.orderService.getOrdersForUser().subscribe({
      next: orders => this.orders = orders
    })
  }

  getAllOrders() {
    this.orderService.getAllOrders().subscribe({
      next: orders => this.orders = orders
    })
  }
}
