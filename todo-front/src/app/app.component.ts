import { TodoService } from './services/todo.service';
import { Component, OnInit } from '@angular/core';
import { Todo } from './models/todo';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public todos: Todo[] = [];
  public newTodo: string;

  constructor(private todoService: TodoService) { }

  ngOnInit() {
    this.todoService.getTodos().subscribe(todos => {
      this.todos = todos;
    });
  }

  addTodo() {
    this.todos.push({ name: this.newTodo, done: false });
    this.newTodo = '';
  }

}


