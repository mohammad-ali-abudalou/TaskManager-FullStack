import { RouterOutlet } from '@angular/router';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet], // Enables routing in the app.
  templateUrl: './app.html',
  styleUrls: [],
})
export class AppComponent {
  title = 'TaskManager';
}
