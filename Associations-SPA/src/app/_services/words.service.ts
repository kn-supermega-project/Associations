import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Words } from '../_interfaces/Words';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WordsService {

baseUrl = environment.apiUrl;


constructor(private http: HttpClient) { }

getAll(): Observable<Words[]> {
  return this.http.get<Words[]>(this.baseUrl + 'words');
}

}
