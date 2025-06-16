import { Component, input, Input } from '@angular/core';

@Component({
  selector: 'app-historic-card',
  imports: [],
  templateUrl: './historic-card.component.html',
  styleUrl: './historic-card.component.scss'
})
export class HistoricCardComponent {
  @Input() title: string = '';
  @Input() description: string = '';
  @Input() date: string = '';
}
