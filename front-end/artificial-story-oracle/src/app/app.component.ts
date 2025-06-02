import { Component } from '@angular/core';
import { RouterOutlet, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { HeaderComponent } from './layouts/header/header.component';
import { FooterComponent } from './layouts/footer/footer.component';

@Component({
  selector: 'app-root',
  standalone: true,  // Muito importante! Indica que é Standalone.
  imports: [
    RouterOutlet, 
    RouterLink, 
    RouterLinkActive, 
    HeaderComponent, 
    FooterComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ASO';

  constructor(private router: Router) {}

  navigateHome() {
    this.router.navigate(['/']);
  }
}
