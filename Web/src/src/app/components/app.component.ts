import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Todos } from "../apis/todosApi";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Taschenka';

  public todos?: Todos.IGetTodoDto[];
  constructor(public todosClient: Todos.Client, public dialog: MatDialog) { }

  ngOnInit() {
    this.todosClient.todosAll().subscribe({
      next: result => {
        this.todos = result;
      },
      error: console.error
    });
  }

  createTodo(): void {
    const dialogRef = this.dialog.open(CreateTodoDialog);

    dialogRef.afterClosed().subscribe((todo: Todos.ICreateTodoDto) => {
      if (todo) {
        console.log(todo);
      }
    });
  }

}

@Component({
  selector: 'create-todo-dialog',
  templateUrl: 'create-todo-dialog.html',
  styleUrls: ['./todo-dialog.css']
})
export class CreateTodoDialog {
  todo: Todos.CreateTodoDto;

  constructor(
    public dialogRef: MatDialogRef<CreateTodoDialog>
  ) {
    this.todo = new Todos.CreateTodoDto({
      name: "",
      description: "",
      deadline: new Date(Date.now()),
      isDone: false,
    })
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
