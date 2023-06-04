import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket.service';
import { BasketItem } from '../shared/models/basket';
import { take } from 'rxjs';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent{
  constructor(public basketService: BasketService) {

  }


  incrementQuantity(item: BasketItem) {
    this.basketService.addItemToBasket(item);
  }

  removeItem(id: string, quantity: number) {
    this.basketService.removeItemFromBasket(id, quantity);
  }
}
