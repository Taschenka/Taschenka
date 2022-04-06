import { Component } from '@angular/core';
import { Todos } from "../apis/todosApi";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Taschenka';
  todosClient: Todos.Client;

  public todos?: Todos.IGetTodoDto[];
  constructor(todosClient: Todos.Client) {
    this.todosClient = todosClient;
  }

  ngOnInit() {
    this.todosClient.todosAll().subscribe({
      next: result => {
        this.todos = result;
      },
      error: console.error
    });
  }
}
