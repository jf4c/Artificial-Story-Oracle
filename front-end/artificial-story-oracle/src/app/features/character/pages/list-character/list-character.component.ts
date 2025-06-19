import { Component } from '@angular/core'
import { CharacterCardComponent } from '../../components/character-card/character-card.component'
import { Character } from '../../models/character.model'
import { CommonModule } from '@angular/common'

@Component({
  selector: 'app-list-character',
  standalone: true,
  imports: [CharacterCardComponent, CommonModule],
  templateUrl: './list-character.component.html',
  styleUrl: './list-character.component.scss',
})
export class ListCharacterComponent {
  myCharacters: Character[] = [
    {
      name: 'Artemis',
      class: 'Mage',
      ancestry: 'Elf',
      level: 5,
      health: 40,
      mana: 80,
      image: 'mage1.png',
    },
    {
      name: 'Ares',
      class: 'Warrior',
      ancestry: 'Human',
      level: 10,
      health: 100,
      mana: 0,
      image: 'warrior1.png',
    },
    {
      name: 'Athena',
      class: 'Rogue',
      ancestry: 'Halfling',
      level: 8,
      health: 60,
      mana: 40,
      image: 'rogue1.png',
    },
    {
      name: 'Hades',
      class: 'Priest',
      ancestry: 'Dwarf',
      level: 12,
      health: 80,
      mana: 120,
      image: 'priest1.png',
    },
    {
      name: 'Zeus',
      class: 'Paladin',
      ancestry: 'Half-Orc',
      level: 15,
      health: 100,
      mana: 50,
      image: 'warrior2.png',
    },
  ]

  handleEdit(character: Character) {
    // faça algo com o personagem editado
    console.log('Editando personagem:', character)
  }

  handleDelete(character: Character) {
    console.log('Delete character:', character.name)
  }
}
