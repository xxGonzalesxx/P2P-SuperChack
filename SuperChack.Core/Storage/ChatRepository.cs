using Microsoft.EntityFrameworkCore;
using SuperChack.Core.Models;

namespace SuperChack.Core.Storage;

public class ChatRepository
{
    private readonly AppDbContext _context;

    public ChatRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
        => await _context.Database.EnsureCreatedAsync();

    public async Task SaveAsync(Message message, CancellationToken ct = default)
    {
        await _context.Messages.AddAsync(message, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<List<Message>> LoadAsync(CancellationToken ct = default)
        => await _context.Messages
            .OrderBy(m => m.SentAt)
            .ToListAsync(ct);
}