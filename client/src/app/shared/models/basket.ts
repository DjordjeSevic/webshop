import * as cuid from "cuid"

export interface Basket {
    id: string
    items: BasketItem[]
    clientSecret?: string;
    paymentIntentId?: string;
    deliveryMethodId?: string;
    shippingPrice: number;
}

export interface BasketItem {
    id: string
    productName: string
    price: number
    quantity: number
    imageUrl: string
    brand: string
    category: string
}

export class Basket implements Basket {
    id = cuid();
    items: BasketItem[] = [];
    shippingPrice = 0;
}

export interface BasketTotals {
    shipping: number;
    subtotal: number;
    total: number;
}