import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { WordRelsToList } from '../_interfaces/WordRelsToList';
import { Observable } from 'rxjs';
import { HttpResponse, HttpParams } from '@angular/common/http';
import { RelWordRequest } from '../RequestModels/RelWord-requst';

@Injectable({
  providedIn: 'root'
})
export class WordRelsService {
private ctrlUrl = 'wordrels';

constructor(private apiService: ApiService) { }

getRelWordsByMainId(id: number, pageSize: number, pageNumber: number): Observable<HttpResponse<WordRelsToList[]>> {
  const params = new HttpParams()
    .set('pageSize', pageSize.toString())
    .set('pageNumber', pageNumber.toString());
  return this.apiService.getFullResponse(`/${this.ctrlUrl}/main/${id}`, params);
}
getById(id: number) {
  return this.apiService.getById(`/${this.ctrlUrl}`, id);
}
create(request: RelWordRequest): Observable<WordRelsToList> {
  return this.apiService.post(`/${this.ctrlUrl}`, request);
}
delete(id: number): Observable<Object> {
  return this.apiService.delete(`/${this.ctrlUrl}/${id}`);
}
update(id: number, request: RelWordRequest): Observable<Object> {
  return this.apiService.put(`/${this.ctrlUrl}/${id}`, request);
}
getByFilter(id: number, searchString: string, pageSize: number, pageNumber: number): Observable<HttpResponse<WordRelsToList[]>> {
  if (searchString === undefined) {
    searchString = '';
  }
  const params = new HttpParams()
    .set('searchString', searchString)
    .set('pageSize', pageSize.toString())
    .set('pageNumber', pageNumber.toString());
  return this.apiService.getFullResponse(`/${this.ctrlUrl}/filtered/${id}`, params);
}
}
