import { CommonModule } from '@angular/common'
import { Component, Input } from '@angular/core'
@Component({
  selector: 'aso-loading',
  imports: [CommonModule],
  templateUrl: './loading.component.html',
  styleUrl: './loading.component.scss',
})
export class LoadingComponent {
  @Input() text = ''
  @Input() inline = false
  @Input() fullscreen = false
}
