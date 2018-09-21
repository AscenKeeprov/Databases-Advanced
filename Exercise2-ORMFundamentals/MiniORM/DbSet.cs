namespace MiniORM
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class DbSet<TEntity> : ICollection<TEntity> where TEntity : class, new()
    {
	internal DbSet(IEnumerable<TEntity> entities)
	{
	    ChangeTracker = new ChangeTracker<TEntity>(entities.ToList());
	    Entities = entities.ToList();
	}

	internal ChangeTracker<TEntity> ChangeTracker { get; set; }
	internal IList<TEntity> Entities { get; private set; }

	public void Add(TEntity entity)
	{
	    if (entity == null) throw new ArgumentNullException(nameof(entity), "Item cannot be null!");
	    Entities.Add(entity);
	    ChangeTracker.Add(entity);
	}

	public void Clear()
	{
	    RemoveRange(Entities);
	}

	public bool Contains(TEntity entity) => Entities.Contains(entity);
	public void CopyTo(TEntity[] array, int arrayIndex) => Entities.CopyTo(array, arrayIndex);
	public int Count => Entities.Count();
	public IEnumerator<TEntity> GetEnumerator() => Entities.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	public bool IsReadOnly => Entities.IsReadOnly;

	public bool Remove(TEntity entity)
	{
	    if (entity == null) throw new ArgumentNullException(nameof(entity), "Item cannot be null!");
	    bool removedSuccessfully = Entities.Remove(entity);
	    if (removedSuccessfully) ChangeTracker.Remove(entity);
	    return removedSuccessfully;
	}

	public void RemoveRange(IEnumerable<TEntity> entities)
	{
	    foreach (TEntity entity in entities)
	    {
		Remove(entity);
	    }
	}

	public void Update(TEntity entity)
	{
	    if (entity == null) throw new ArgumentNullException(nameof(entity), "Item cannot be null!");
	    ChangeTracker.Update(entity);
	}
    }
}
