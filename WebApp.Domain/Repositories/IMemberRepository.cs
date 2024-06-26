﻿using WebApp.Domain.Entities;
using WebApp.Domain.QueryObjects;
using WebApp.Domain.ValueObjects;

namespace WebApp.Domain.Repositories;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Member>> GetAllMembersAsync(MemberQueryObject queryObject, CancellationToken cancellationToken = default);

    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);

    void Add(Member member);

    void Update(Member member);
}
