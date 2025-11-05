﻿using ASO.Application.Abstractions.Shared;
using ASO.Application.UseCases.Oracle.GenerateNames;

namespace ASO.Application.Abstractions.UseCase.Oracle;

public interface IGenerateCharactersNames 
    : ICommandHandlerAsync<GenerateCharacterNamesCommand, GenerateCharacterNamesResponse>;
