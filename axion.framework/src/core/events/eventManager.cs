namespace Axion;

/// <summary>
/// EventManager class responsible of handling the different events of the game
/// </summary>
public class EventManager {
    Dictionary<string, Action<object, EventArgs>> handlers;
    internal EventManager() {
        handlers = new Dictionary<string, Action<object, EventArgs>>();
    }

    /// <summary>
    /// Dispatch an event
    /// </summary>
    /// <param name="eventName">Name of the event</param>
    /// <param name="args">Arguments to ass</param>
    public void DispatchEvent(string eventName, EventArgs args) {
        if (handlers.ContainsKey(eventName)) {
            handlers[eventName].Invoke(this, args);
        }
    }

    /// <summary>
    /// Add an event listener
    /// </summary>
    /// <param name="eventName">Name of the event</param>
    /// <param name="eventAction">Handler function</param>
    public void AddEventListener(string eventName, Action<object, EventArgs> eventAction) {
        if (!handlers.ContainsKey(eventName)) {
            handlers.Add(eventName, eventAction);
        } else {
            handlers[eventName] += eventAction;
        }
    }

    /// <summary>
    /// Remove an event listener
    /// </summary>
    /// <param name="eventName">Name of the event</param>
    /// <param name="eventAction">Handler function</param>
    public void RemoveEventListener(string eventName, Action<object, EventArgs> eventAction) {
        if (handlers.ContainsKey(eventName)) {
            handlers[eventName] -= eventAction;
        }
    }
}