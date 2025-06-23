import { Component, Input } from '@angular/core'

@Component({
  selector: 'aso-feature-card',
  imports: [],
  templateUrl: './feature-card.component.html',
  styleUrl: './feature-card.component.scss',
})
export class FeatureCardComponent {
  @Input() icon = ''
  @Input() title = ''
  @Input() description = ''
}
