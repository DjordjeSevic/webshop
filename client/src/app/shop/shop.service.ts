import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { Brand } from '../shared/models/brand';
import { Category } from '../shared/models/category';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl : string = 'https://localhost:7255/api/';

  constructor(private http: HttpClient) { }

  getProduct(id: string) {
    return this.http.get<Product>(this.baseUrl + 'products/' + id);
  }

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();
    
    if (shopParams.brandId) params = params.append('brandId', shopParams.brandId);
    if (shopParams.categoryId) params = params.append('categoryId', shopParams.categoryId);
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber);
    params = params.append('pageSize', shopParams.pageSize);
    if (shopParams.search) params = params.append('search', shopParams.search);

    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'products', {params});
  }

  getBrands() {
    return this.http.get<Brand[]>(this.baseUrl + 'brands');
  }

  getCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'categories');
  }
}
