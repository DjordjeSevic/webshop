import { Address } from "./user"

export interface OrderToCreate {
    basketId: string;
    deliveryMethodId: string;
    shipToAddress: Address;
}

export interface Order {
    id: string
    buyerEmail: string
    orderDate: string
    shipToAddress: Address
    deliveryMethod: string
    orderItems: OrderItem[]
    shippingPrice: number
    subtotal: number
    total: number
    status: string
  }
  
  export interface OrderItem {
    productId: string
    productName: string
    imageUrl: string
    price: number
    quantity: number
  }