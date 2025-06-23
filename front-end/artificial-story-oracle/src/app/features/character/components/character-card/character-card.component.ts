import { Component, EventEmitter, Input, Output } from '@angular/core'
import { Character } from '../../models/character.model'
import { CommonModule } from '@angular/common'

@Component({
  selector: 'aso-character-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './character-card.component.html',
  styleUrl: './character-card.component.scss',
})
export class CharacterCardComponent {
  @Input() character!: Character

  @Output() edit = new EventEmitter<void>()
  @Output() delete = new EventEmitter<void>()

  onEdit() {
    this.edit.emit()
  }

  onDelete() {
    this.delete.emit()
  }
}
