import { Component } from '@angular/core'
import { FeatureCardComponent } from './components/feature-card/feature-card.component'
import { HistoricCardComponent } from './components/historic-card/historic-card.component'

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FeatureCardComponent, HistoricCardComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {}
