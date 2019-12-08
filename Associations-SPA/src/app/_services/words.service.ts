import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Words } from "../_interfaces/Words";
import { HttpClient, HttpResponse, HttpParams } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { ApiService } from "./api.service";
import { MainWordRequest } from "../RequestModels/MainWord-request";

@Injectable()
export class WordsService {
  private ctrlUrl = 'words';

  constructor(private apiService: ApiService) {}

  getAll(): Observable<Words[]> {
    return this.apiService.get(`/${this.ctrlUrl}`);
  }
  getById(id: number) {
    return this.apiService.getById(`/${this.ctrlUrl}/`, id);
  }
  getMainWords(pageSize: number, pageNumber: number): Observable<HttpResponse<Words[]>> {
    const params = new HttpParams()
    .set('pageSize', pageSize.toString())
    .set('pageNumber', pageNumber.toString());
  return this.apiService.getFullResponse(`/${this.ctrlUrl}`, params);
  }
  create(request: MainWordRequest): Observable<Words> {
    return this.apiService.post(`/${this.ctrlUrl}`, request);
  }
  update(id: number, request: MainWordRequest): Observable<Object> {
    return this.apiService.put(`/${this.ctrlUrl}/${id}`, request);
  }
  delete(id: number): Observable<Object> {
    return this.apiService.delete(`/${this.ctrlUrl}/${id}`);
  }
  getByFilter(searchString: string, pageSize: number, pageNumber: number): Observable<HttpResponse<Words[]>> {
    if (searchString === undefined) {
      searchString = '';
    }
    const params = new HttpParams()
      .set('searchString', searchString)
      .set('pageSize', pageSize.toString())
      .set('pageNumber', pageNumber.toString());
    return this.apiService.getFullResponse(`/${this.ctrlUrl}/filtered`, params);
}
}
