import React, { Component } from 'react';
import { loginCommand, listNovelsCommand, addNovelCommand, listGenresCommand, addGenreCommand, listChapterCommand, addChapterCommand, logoutCommand } from '../business/command-line-business';

export default class CommandLine extends Component<{}, CommandLineState> {
    state: CommandLineState = {
        command: '',
        commands: []
    }

    _addCommand = (e: any) => {
        e.preventDefault();
        var command = this.state.command;
        this.setState({ commands: [...this.state.commands, `> ${command}`], command: '' });
        this._processCommand(command);
    }

    _processCommand = async (cmd: string) => {
        var result: string[] = [];
        if (cmd === "list novels") {
            result = await listNovelsCommand();
        }
        else if (cmd === "list genres") {
            result = await listGenresCommand();
        }
        else if (cmd.startsWith("login")) {
            result = await loginCommand(cmd);
        }
        else if (cmd === "logout") {
            result = await logoutCommand();
        }
        else if (cmd.startsWith("create novel")) {
            result = await addNovelCommand(cmd);
        }
        else if (cmd.startsWith("create genre")) {
            result = await addGenreCommand(cmd);
        }
        else if (cmd.startsWith("create chapter")) {
            result = await addChapterCommand(cmd);
        }
        else if (cmd.startsWith("list chapters")) {
            result = await listChapterCommand(cmd);
        }
        else result.push(`> Invalid command '${cmd}'`);
        this.setState({ commands: [...this.state.commands, ...result] }, this._scrollToBottom);
    }

    _scrollToBottom = () => {
        var objDiv = document.getElementById("terminal");
        if (objDiv !== null) objDiv.scrollTop = objDiv.scrollHeight;
    }

    render() {
        return (
            <>
                <div id="terminal" className="terminal">
                    {this.state.commands.map((cmd, index) =>
                        <code key={index}>{cmd}</code>
                    )}
                    <form onSubmit={this._addCommand.bind(this)}>
                        <input placeholder=">_" type="text" value={this.state.command} onChange={e => { this.setState({ command: e.target.value }) }} />
                    </form>
                </div>
                <div className="instruction">
                    <h4>Commands:</h4>
                    <ul>
                        <li><code>login "username" "password"</code> Login to the system, (credentials username: 123, password: 123)</li>
                        <li><code>list novels</code> List all novels</li>
                        <li><code>list genres</code> List all genres</li>
                        <li><code>create novel "title" "author" "description"</code> Creates a new novel. Title, author, description is required</li>
                        <li><code>create genre "name"</code> Creates a new genre. Name is required</li>
                        <li><code>create chapter "NovelId", "ChapterNumber", "Title", "Content"</code> Creates a new chapter for a novel. NovelId, ChapterNumber, Title, Content are required</li>
                        <li><code>list chapters NovelId</code> List all chapters of a novel. NovelId int required</li>
                    </ul>
                </div>
            </>
        )
    }
}

interface CommandLineState {
    commands: string[];
    command: string;
}