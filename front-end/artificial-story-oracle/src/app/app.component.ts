import { Component, inject } from '@angular/core'
import {
  RouterOutlet,
  Router,
  RouterLink,
  RouterLinkActive,
} from '@angular/router'
import { HeaderComponent } from './core/layouts/header/header.component'
import { FooterComponent } from './core/layouts/footer/footer.component'

@Component({
  selector: 'aso-root',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    HeaderComponent,
    FooterComponent,
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'ASO'
  currentTheme: 'light' | 'dark' = 'dark'

  private router = inject(Router)

  constructor() {
    this.setTheme(this.currentTheme)
  }

  navigateHome() {
    this.router.navigate(['/'])
  }

  setTheme(theme: 'light' | 'dark') {
    document.body.classList.remove('light-theme', 'dark-theme')
    document.body.classList.add(`${theme}-theme`)
    this.currentTheme = theme
  }

  // ✅ Método para alternar entre light e dark
  toggleTheme() {
    const newTheme = this.currentTheme === 'light' ? 'dark' : 'light'
    this.setTheme(newTheme)
  }
}
