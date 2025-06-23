import { CommonModule } from '@angular/common'
import { Component, Input } from '@angular/core'
import { LoadingComponent } from '../loading/loading.component'

@Component({
  selector: 'aso-dropdown-input',
  standalone: true,
  imports: [CommonModule, LoadingComponent],
  templateUrl: './dropdown-input.component.html',
  styleUrl: './dropdown-input.component.scss',
})
export class DropdownInputComponent {
  @Input() options: string[] = []
  @Input() placeholder = 'Selecione...'
  @Input() isLoading = false

  isOpen = false
  selectedOption: string | null = null
  constructor() {
    console.log('DropdownInputComponent initialized')
    console.log(this.selectedOption)
  }
  toggleDropdown() {
    this.isOpen = !this.isOpen
  }

  clearSelection(event: MouseEvent) {
    event.stopPropagation()
    this.selectedOption = null
  }

  selectOption(option: string) {
    this.selectedOption = option
    this.isOpen = false

    console.log(this.selectedOption)
  }
}
