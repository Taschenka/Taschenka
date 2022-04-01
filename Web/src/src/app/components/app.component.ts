import { Component } from '@angular/core';
import { todos as Todos } from "../apis/todosApi";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Taschenka';

  public todos?: Todos.IGetTodoDto[];
  constructor(todosClient: Todos.Client) {
    todosClient.todosAll().subscribe(
      result => {
        this.todos = result;
      },
      error => console.error(error)
    );
  }
}
