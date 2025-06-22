import { Component, inject, OnInit } from '@angular/core'
import { CommonModule } from '@angular/common'
import { CharacterCardComponent } from '@features/character/components/character-card/character-card.component'
import { AncestryService } from '@features/character/services/ancestry.service'
import { ClassService } from '@features/character/services/class.service'
import { Ancestries, Ancestry } from '@features/character/models/ancestry.model'
import { Class, Classes } from '@features/character/models/class.model'
import { Character } from '@features/character/models/character.model'
import { InputComponent } from '@shared/components/input/input.component'
import { DropdownInputComponent } from '@shared/components/dropdown-input/dropdown-input.component'

@Component({
  selector: 'app-list-character',
  standalone: true,
  imports: [
    CharacterCardComponent,
    InputComponent,
    DropdownInputComponent,
    CommonModule,
  ],
  templateUrl: './list-character.component.html',
  styleUrl: './list-character.component.scss',
})
export class ListCharacterComponent implements OnInit {
  private ancestryService = inject(AncestryService)
  private classService = inject(ClassService)
  ancestries: Ancestry[] = []
  classes: Class[] = []

  constructor() {
    console.log('ListCharacterComponent constructed')
  }

  ngOnInit(): void {
    this.ancestryService.getAncestries().subscribe({
      next: (data: Ancestries) => {
        this.ancestries = data.ancestries as Ancestry[]
      },
      error: (error: unknown) => {
        console.error('Erro ao carregar as raças:', error)
      },
    })
    this.classService.getClasses().subscribe({
      next: (data: Classes) => {
        this.classes = data.classes as Class[]
      },
      error: (error: unknown) => {
        console.error('Erro ao carregar as classes:', error)
      },
    })
  }

  getAncestryNames(): string[] {
    return this.ancestries?.map((ancestry) => ancestry.name)
  }

  getClassNames(): string[] {
    return this.classes?.map((c) => c.name)
  }

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
