import { TodoService } from './services/todo.service';
import { Component, OnInit } from '@angular/core';
import { Todo } from './models/todo';
import { finalize } from 'rxjs/operators';
import { LoggingService } from './services/logging.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public todos: Todo[] = [];
  public newTodo: string;
  loading: boolean;

  constructor(private todoService: TodoService, private loggingService: LoggingService) { }

  ngOnInit() {
    this.todoService.getTodos().subscribe(todos => {
      this.todos = todos;
    });
  }

  addTodo() {
    this.todos.push({ id: null, name: this.newTodo, done: false });
    this.loggingService.logTrace(`New todo "${this.newTodo}" added`);
    this.newTodo = '';
  }

  saveTodos() {
    this.loading = true;
    this.todoService.saveTodos(this.todos)
      .pipe(finalize(() => this.loading = false))
      .subscribe(todos => this.todos = todos);

    this.loggingService.logTrace('Todos sent to the server');
  }

}


