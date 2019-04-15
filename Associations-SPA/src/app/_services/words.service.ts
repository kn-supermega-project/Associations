import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Words } from "../_interfaces/Words";
import { HttpClient, HttpResponse, HttpParams } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { WordsToList } from "../_interfaces/WordsToList";
import { ApiService } from "./api.service";

@Injectable()
export class WordsService {
  private ctrlUrl = 'words';

  constructor(private apiService: ApiService) {}

  getAll(): Observable<Words[]> {
    return this.apiService.get(`${this.ctrlUrl}`);
  }
  getById(id: number) {
    return this.apiService.getById(`${this.ctrlUrl}/`, id)
  }
  getMainWords(pageSize: number, pageNumber: number): Observable<HttpResponse<WordsToList[]>> {
    const params = new HttpParams()
    .set('pageSize', pageSize.toString())
    .set('pageNumber', pageNumber.toString());
  return this.apiService.getFullResponse(`${this.ctrlUrl}/main`, params);
  }

}
