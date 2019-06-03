import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { WordRelsToList } from '../_interfaces/WordRelsToList';
import { Observable } from 'rxjs';
import { HttpResponse, HttpParams } from '@angular/common/http';
import { RelWordRequest } from '../RequestModels/RelWord-requst';
import { WordsRel } from '../_interfaces/WordsRel';

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
  return this.apiService.getFullResponse(`/${this.ctrlUrl}/${id}`, params);
}
create(request: RelWordRequest): Observable<WordsRel> {
  return this.apiService.post(`/${this.ctrlUrl}`, request);
}

}
