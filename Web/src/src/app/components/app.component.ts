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

    dialogRef.afterClosed().subscribe((todo: Todos.CreateTodoDto) => {
      if (todo) {
        console.log(todo);
        this.todosClient.todosPOST(todo);
        this.todos?.push(todo)
      }
    });
  }

  editTodo(todo: Todos.IGetTodoDto): void {
    const dialogRef = this.dialog.open(EditTodoDialog, { data: todo });

    dialogRef.afterClosed().subscribe((updated: Todos.UpdateTodoDto) => {
      if (updated) {
        this.todosClient.todosPUT(todo.id!, updated).subscribe({
          error: console.error
        });
        let index = this.todos!.findIndex(it => it.id == todo.id)
        this.todos![index] = updated
      }
    });
  }

  deleteTodo(id: string) {
    this.todosClient.todosDELETE(id).subscribe({
      error: console.error
    });
    let index = this.todos!.findIndex(it => it.id == id)
    this.todos?.splice(index, 1)
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

@Component({
  selector: 'edit-todo-dialog',
  templateUrl: 'edit-todo-dialog.html',
  styleUrls: ['./todo-dialog.css']
})
export class EditTodoDialog {
  todo: Todos.UpdateTodoDto;

  constructor(
    public dialogRef: MatDialogRef<EditTodoDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Todos.IGetTodoDto,
  ) {
    this.todo = new Todos.UpdateTodoDto({
      name: data.name,
      description: data.description,
      deadline: data.deadline,
      isDone: data.isDone
    })
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}