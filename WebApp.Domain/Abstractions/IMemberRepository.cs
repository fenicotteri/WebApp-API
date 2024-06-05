﻿using WebApp.Domain.Entities;
using WebApp.Domain.Entities;

namespace WebApp.Domain.Abstractions;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);

    void Add(Member member);

    void Update(Member member);

}