import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Todo } from './../models/todo';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class TodoService {

  constructor(private http: HttpClient) { }

  public getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(`${environment.apiUrl}/todos`);
  }

  public saveTodos(todos: Todo[]): Observable<Todo[]> {
    return this.http.post<Todo[]>(`${environment.apiUrl}/todos`, todos);
  }
}
