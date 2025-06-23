import { Component, Input } from '@angular/core'

@Component({
  selector: 'aso-input',
  imports: [],
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss'],
})
export class InputComponent {
  @Input() placeholder = ''
  @Input() icon = ''
  @Input() type: 'text' | 'password' | 'email' = 'text'
  @Input() method: (params: unknown) => unknown = () => undefined
}
