using System;
using System.Collections.Generic;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace Blog.Infrastructure.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        //public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<JwtToken> Tokens => Set<JwtToken>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Category> Сategories => Set<Category>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = "server=localhost;port=3306;user=root;password=56537;database=NewMyBlog";
            //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            //const string dbPath = "myapp.db";
            //optionsBuilder.UseSqlite($"Data Source={dbPath};");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            FillDataBaseWithInitialData(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private static void FillDataBaseWithInitialData(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Role>().HasData(
                new[]
                {
                    new Role()
                    {
                        Id = 1,
                        Name = "admin"
                    },
                    new Role()
                    {
                        Id = 2,
                        Name = "user"
                    },
                });



            modelBuilder.Entity<Account>().HasData(
                new[]
                {
                    new Account()
                    {
                        Id = 1,
                        Login = "admin",
                        PasswordHash = "AQAAAAEAACcQAAAAEAd+xuTj0l1UjsuWNC68cA9rqd/oXPEDD1+eGkOLA7OYddgFbAptRGrTWbuEvHfi/Q==",
                        Email = "eupftxzounoehg@arxxwalls.com",
                        RoleId = 1,
                        Registered = DateTime.Now
                    },
                    new Account()
                    {
                        Id = 2,
                        Login = "joker",
                        PasswordHash = "AQAAAAEAACcQAAAAEAd+xuTj0l1UjsuWNC68cA9rqd/oXPEDD1+eGkOLA7OYddgFbAptRGrTWbuEvHfi/Q==",
                        Email = "joker@mail.ru",
                        RoleId = 2,
                        Registered = DateTime.Now
                    },

                });

            modelBuilder.Entity<Category>().HasData(
                new[]
                {
                    new Category()
                    {
                        Id=1,
                        Name="Общее"

                    },
                    new Category()
                    {
                        Id=2,
                        Name="Технологии"

                    },
                    new Category()
                    {
                        Id=3,
                        Name="Разработка"

                    },
                    new Category()
                    {
                        Id=4,
                        Name="Дизайн"

                    },
                    new Category()
                    {
                        Id=5,
                        Name="Администрирование"

                    },
                    new Category()
                    {
                        Id=6,
                        Name="Маркетинг"

                    },
                    new Category()
                    {
                        Id=7,
                        Name="Научпоп"

                    },
                }
            );

            AddPosts(modelBuilder);
        }


        private static void AddPosts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasData(
                new[]
                {
                    new Post()
                    {
                        Id = 1,
                        AccountId = 2,
                        CategoryId = 3,
                        LastChange = DateTime.Now,
                        IsAllowCommenting = false,
                        IsVisibleAll = true,
                        Views = 0,
                        Status = PostStatus.Publish,
                        Title = "Angular. Понимание @Input, @Output и EventEmitter",
                        Anons = @"Привет! Представляю вашему вниманию перевод статьи «Understanding @Input, @Output 
                                and EventEmitter in Angular» автора foolishneo.",
                        FullText = @"<p>Привет, Хабр! Представляю вашему вниманию перевод статьи <a href='https://medium.com/@foolishneo/understanding-input-output-and-eventemitter-in-angular-c1aeb9fff594'>«Understanding @Input, @Output and EventEmitter in Angular»</a> автора foolishneo.<br>
<br>
Приветствую всех желающих накапливать информацию о тонкостях работы фреймворка Angular.<br>
<br>
Сегодня посчитал нужным лучше изучить информацию, касающуюся организации взаимодействия между компонентами Angular и начать практиковать переводы интересующих меня статей с английского.<br>
<br>
Надеюсь найдутся те, для кого перевод статьи с <a href='https://medium.com/@foolishneo/understanding-input-output-and-eventemitter-in-angular-c1aeb9fff594'>Medium</a> будет полезен. <br>
<br>
Давно имея желание начать переводить полезные статьи с английского, решил начать с простенькой, весьма не объемной, но возможно, дополняющей имеющиеся знания статейки. К Вашему вниманию, господа… <a name='habracut'></a><br>
<br>
Для тех, кто новенький в изучении Angular, декораторы <i>Input</i> и <i>Output</i> могут вызывать смущение, особенно, когда Вы пытаетесь разобраться в их назначении с помощью примеров кода. В этой статье, я попытаюсь объяснить их максимально простым способом. <br>
<br>
<h2>Инструмент для обмена данными</h2><br>
В первую очередь, задача декораторов <i>Input</i> и <i>Output</i> состоит в обмене данными между компонентами. Они являются механизмом для получения/отправки данных от одного компонента к другому. <br>
<br>
<i>Input </i>используется для получения данных, в то время как <i>Output </i> для их отправки. <i>Output </i> отправляет данные выставляя их в качестве производителей событий, обычно как объекты класса <i>EventEmitter.</i><br>
<br>
Таким образом, когда Вы видите код, на подобии этого:<br>
<br>
<pre><code class='plaintext hljs'>@Component({
  selector: 'todo-item',
  ...
})
export class TodoItemComponent {
  @Input()  item
  @Output() onChange = new EventEmitter()
}
</code></pre><br>
он значит: <br>
<br>
<ul>
<li>Эй, я ожидаю отправленные мне данные. Я получу их и сохраню в качестве значения свойства <i>item.</i><br>
</li>
<li>Кстати, я буду причиной отправки данных с помощью свойства <i>onChange.</i><br>
</li>
</ul><br>
Давайте представим что у Вас есть компонент <i>TodoList</i>, который содержит компонент <i>TodoItem</i>. <br>
В шаблоне компонента<i>TodoLis</i>t Вы ожидаете увидеть: <br>
<br>
<pre><code class='html hljs xml'>...
<span class='hljs-tag'>&lt;<span class='hljs-name'>todo-item</span>
  [<span class='hljs-attr'>item</span>]=<span class='hljs-string'>'myTask'</span>
  (<span class='hljs-attr'>onChange</span>)=<span class='hljs-string'>'handleChange($event)'</span>
&lt;/<span class='hljs-attr'>todo-item</span>&gt;</span>
...</code></pre><br>
что значит: <br>
<br>
<ul>
<li> Компонент <i>TodoList</i> помещает значение данных в принадлежащее ему свойство <i>myTask</i> и передает его компоненту <b><i>TodoItem</i></b><br>
</li>
<li> Данные, переданные с компонента <i>TodoItem</i>, будут получены и обработаны функцией <i>handleChange()</i> компонента <i>TodoList</i> <br>
</li>
</ul><br>
Хватит теории. Давайте посмотрим на пример. <br>
<br>
<h2>@Input и <a href='https://habr.com/ru/users/output/' class='user_link'>Output</a> в действии.</h2><br>
Обратите внимание на <a href='http://stackblitz.com/edit/angular-simple-input-output'>пример.</a><br>
<br>
Здесь я создал 2 компонента, компонент <i>hello</i>, вложенный в компонент <i>app</i>. Компонент <i>hello</i> имеет <i>Input</i> и <i>Output</i>:<br>
<br>
<pre><code class='plaintext hljs'>hello.component.ts
@Component({
  selector: 'hello',
  template: `
    &lt;h3 (click)='onClick.emit('Neo')'&gt;
       ...
    &lt;/h3&gt;
  `
})
export class HelloComponent  {
  @Input() myFriend: string
  @Output() onClick = new EventEmitter()
}
</code></pre><br>
Компонент <i>hello</i> ожидает получить значение типа «строка» и поместить его в качестве значения свойства <i>myFriend</i>.<br>
<br>
<pre><code class='plaintext hljs'>@Input() myFriend: string
</code></pre><br>
Каждый раз, когда Вы кликните на него, свойство отправки данных <i>onClick</i> декоратора <i>Output</i> передаст «внешнему миру» строку с содержанием «Neo».<br>
<br>
<pre><code class='plaintext hljs'>&lt;h3 (click)='onClick.emit('Neo')'&gt;
</code></pre><br>
Ниже расположен код компонента <i>app</i>:<br>
<br>
<pre><code class='plaintext hljs'>app.component.ts
export class AppComponent  {
  ng = 'Angular'
  myName = 'Neo'
  upCase(st:string): void { ... }
}

app.component.html
&lt;hello myFriend='{{ ng }}' (onClick)='upCase($event)'&gt;&lt;/hello&gt;
&lt;h3&gt;My name is {{ myName }}&lt;/h3&gt;
</code></pre><br>
Обратите внимание что компонент <i>app</i> использует тег компонента <i>hello</i> в своем шаблоне, который совершает 2 действия:<br>
 <br>
<ul>
<li>передает значение строки <i>'Angular'</i> компоненту <i>hello</i> с помощью свойства <i>ng</i><br>
</li>
<li>и получает отправленное значение от компонента hello и обрабатывает полученное значение с помощью функции upCase():<br>
</li>
</ul><br>
<pre><code class='plaintext hljs'>&lt;hello myFriend='{{ ng }}' (onClick)='upCase($event)'&gt;
</code></pre><br>
Вы можете увидеть приложение в действии <a href='https://angular-simple-input-output.stackblitz.io/'>здесь.</a></p>"
                    },
                    new Post()
                    {
                        Id = 2,
                        AccountId = 2,
                        CategoryId = 3,
                        LastChange = DateTime.Now,
                        IsAllowCommenting = false,
                        IsVisibleAll = true,
                        Views = 0,
                        Status = PostStatus.Publish,
                        Title = "Webpack: руководство для начинающих",
                        Anons = @"Представляю вашему вниманию перевод статьи «Webpack: A gentle introduction» автора Tyler McGinnis.",
                        FullText = @"<p>Доброго времени суток, друзья! <br>
<br>
Представляю вашему вниманию перевод статьи <a href='https://ui.dev/webpack/' rel='nofollow'>«Webpack: A gentle introduction»</a> автора Tyler McGinnis. <br>
<br>
Перед изучением новой технологии задайте себе два вопроса:<br>
<br>
</p><ol>
<li>Зачем нужен этот инструмент?</li>
<li>Какие задачи он выполняет?</li>
</ol><br>
Если вы не можете ответить на эти вопросы, возможно, вам не нужна изучаемая технология. Давайте попробуем ответить на эти вопросы применительно к Webpack.<br>
<a name='habracut'></a><br>
<h3>Зачем нужен вебпак?</h3><br>
Вебпак — это сборщик модулей. Он анализирует модули приложения, создает граф зависимостей, затем собирает модули в правильном порядке в один или более бандл (bundle), на который может ссылаться файл «index.html». <br>
<br>
<pre><code class='javascript hljs'>App.js -&gt;       |
Dashboard.js -&gt; | Bundler | -&gt; bundle.js
About.js -&gt;     |
</code></pre><br>
<h3>Какие проблемы решает вебпак?</h3><br>
Обычно, при создании приложения на JavaScript, код разделяется на несколько частей (модулей). Затем в файле «index.html» необходимо указать ссылку на каждый скрипт. <br>
<hr>
<pre><code class='xml hljs'><span class='hljs-tag'>&lt;<span class='hljs-name'>body</span>&gt;</span>

    ...
    
    <span class='hljs-tag'>&lt;<span class='hljs-name'>script</span> <span class='hljs-attr'>src</span>=<span class='hljs-string'>'http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.4/jquery.min.js'</span>&gt;</span><span class='hljs-tag'>&lt;/<span class='hljs-name'>script</span>&gt;</span>
    <span class='hljs-tag'>&lt;<span class='hljs-name'>script</span> <span class='hljs-attr'>src</span>=<span class='hljs-string'>'libs/react.min.js'</span>&gt;</span><span class='hljs-tag'>&lt;/<span class='hljs-name'>script</span>&gt;</span>
    <span class='hljs-tag'>&lt;<span class='hljs-name'>script</span> <span class='hljs-attr'>src</span>=<span class='hljs-string'>'src/admin.js'</span>&gt;</span><span class='hljs-tag'>&lt;/<span class='hljs-name'>script</span>&gt;</span>
    <span class='hljs-tag'>&lt;<span class='hljs-name'>script</span> <span class='hljs-attr'>src</span>=<span class='hljs-string'>'src/dashboard.js'</span>&gt;</span><span class='hljs-tag'>&lt;/<span class='hljs-name'>script</span>&gt;</span>
    <span class='hljs-tag'>&lt;<span class='hljs-name'>script</span> <span class='hljs-attr'>src</span>=<span class='hljs-string'>'src/api.js'</span>&gt;</span><span class='hljs-tag'>&lt;/<span class='hljs-name'>script</span>&gt;</span>
    <span class='hljs-tag'>&lt;<span class='hljs-name'>script</span> <span class='hljs-attr'>src</span>=<span class='hljs-string'>'src/auth.js'</span>&gt;</span><span class='hljs-tag'>&lt;/<span class='hljs-name'>script</span>&gt;</span>
    <span class='hljs-tag'>&lt;<span class='hljs-name'>script</span> <span class='hljs-attr'>src</span>=<span class='hljs-string'>'src/rickastley.js'</span>&gt;</span><span class='hljs-tag'>&lt;/<span class='hljs-name'>script</span>&gt;</span>
<span class='hljs-tag'>&lt;/<span class='hljs-name'>body</span>&gt;</span>
</code></pre><hr>
Это не только утомительно, но и подвержено ошибкам. Важно не только не забыть про какой-нибудь скрипт, но и расположить их в правильном порядке. Если загрузить скрипт, зависящий от React, до загрузки самого React, приложение сломается. Вебпак решает эти задачи. Не нужно беспокоиться о последовательном включении всех скриптов. <br>
<hr>
<pre><code class='xml hljs'><span class='hljs-tag'>&lt;<span class='hljs-name'>body</span>&gt;</span>

    ...
    
    <span class='hljs-tag'>&lt;<span class='hljs-name'>script</span> <span class='hljs-attr'>src</span>=<span class='hljs-string'>'dist/bundle.js'</span>&gt;</span><span class='hljs-tag'>&lt;/<span class='hljs-name'>script</span>&gt;</span>
<span class='hljs-tag'>&lt;/<span class='hljs-name'>body</span>&gt;</span>
</code></pre><hr>
Как мы скоро узнаем, сбор модулей является лишь одним из аспектов работы вебпака. При необходимости можно заставить вебпак осуществить некоторые преобразования модулей перед их добавлением в бандл. Например, преобразование SASS/LESS в обычный CSS, или современного JavaScript в ES5 для старых браузеров. <br>
<br>
<h3>Установка вебпака</h3><br>
После инициализации проекта с помощью npm, для работы вебпака нужно установить два пакета — <code>webpack</code> и <code>webpack-cli</code>. <br>
<br>
<pre><code class='javascript hljs'>npm i webpack webpack-cli -D
</code></pre><br>
<h3>webpack.config.js</h3><br>
После установки указанных пакетов, вебпак нужно настроить. Для этого создается файл <code>webpack.config.js</code>, который экспортирует объект. Этот объект содержит настройки вебпака. <br>
<br>
<pre><code class='javascript hljs'><span class='hljs-built_in'>module</span>.exports = {}
</code></pre><br>
Основной задачей вебпака является анализ модулей, их опциональное преобразование и интеллектуальное объединение в один или более бандл, поэтому вебпаку нужно знать три вещи: <br>
<br>
<ol>
<li>Точка входа приложения</li>
<li>Преобразования, которые необходимо выполнить</li>
<li>Место, в которое следует поместить сформированный бандл</li>
</ol><br>
<h3>Точка входа</h3><br>
Сколько бы модулей не содержало приложение, всегда имеется единственная точка входа. Этот модуль включает в себя остальные. Обычно, таким файлом является index.js. Это может выглядеть так: <br>
<br>
<pre><code class='javascript hljs'>index.js
  imports about.js
  imports dashboard.js
    imports graph.js
    imports auth.js
      imports api.js
</code></pre><br>
Если мы сообщим вебпаку путь до этого файла, он использует его для создания графа зависимостей приложения. Для этого необходимо добавить свойство <code>entry</code> в настройки вебпака со значением пути к главному файлу: <br>
<br>
<pre><code class='javascript hljs'><span class='hljs-built_in'>module</span>.exports = {
    <span class='hljs-attr'>entry</span>: <span class='hljs-string'>'./app/index.js'</span>
}
</code></pre><br>
<h3>Преобразования с помощью лоадеров (loaders)</h3><br>
После добавления точки входа, нужно сообщить вебпаку о преобразованиях, которые необходимо выполнить перед генерацией бандла. Для этого используются лоадеры. <br>
<br>
По умолчанию при создании графика зависимостей на основе операторов <code>import / require()</code> вебпак способен обрабатывать только JavaScript и JSON-файлы. <br>
<br>
<pre><code class='javascript hljs'><span class='hljs-keyword'>import</span> auth <span class='hljs-keyword'>from</span> <span class='hljs-string'>'./api/auth'</span> <span class='hljs-comment'>// </span>
<span class='hljs-keyword'>import</span> config <span class='hljs-keyword'>from</span> <span class='hljs-string'>'./utils/config.json'</span> <span class='hljs-comment'>// </span>
<span class='hljs-keyword'>import</span> <span class='hljs-string'>'./styles.css'</span> <span class='hljs-comment'>// ️</span>
<span class='hljs-keyword'>import</span> logo <span class='hljs-keyword'>from</span> <span class='hljs-string'>'./assets/logo.svg'</span> <span class='hljs-comment'>// ️</span>
</code></pre><br>
Едва ли в своем приложении вы решитесь ограничиться JS и JSON-файлами, скорее всего, вам также потребуются стили, SVG, изображения и т.д. Вот где нужны лоадеры. Основной задачей лоадеров, как следует из их названия, является предоставление вебпаку возможности работать не только с JS и JSON-файлами. <br>
<br>
Первым делом нужно установить лоадер. Поскольку мы хотим загружать SVG, с помощью npm устанавливаем svg-loader. <br>
<br>
<pre><code class='javascript hljs'>npm i svg-inline-loader -D 
</code></pre><br>
Далее добавляем его в настройки вебпака. Все лоадеры включаются в массив объектов <code>module.rules</code>: <br>
<br>
<pre><code class='javascript hljs'><span class='hljs-built_in'>module</span>.exports = {
    <span class='hljs-attr'>entry</span>: <span class='hljs-string'>'./app/index.js'</span>,
    <span class='hljs-attr'>module</span>: {
        <span class='hljs-attr'>rules</span>: []
    }
}
</code></pre><br>
Информация о лоадере состоит из двух частей. Первая — тип обрабатываемых файлов (<code>.svg</code> в нашем случае). Вторая — лоадер, используемый для обработки данного типа файлов (<code>svg-inline-loader</code> в нашем случае). <br>
<br>
<pre><code class='javascript hljs'><span class='hljs-built_in'>module</span>.exports = {
  <span class='hljs-attr'>entry</span>: <span class='hljs-string'>'./app/index.js'</span>,
  <span class='hljs-attr'>module</span>: {
    <span class='hljs-attr'>rules</span>: [
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.svg$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'svg-inline-loader'</span> }
    ]
  }
}
</code></pre><br>
Теперь мы можем импортировать SVG-файлы. Но что насчет наших CSS-файлов? Для стилей используется <code>css-loader</code>. <br>
<br>
<pre><code class='javascript hljs'>npm i css-loader -D 
</code></pre><br>
<pre><code class='javascript hljs'><span class='hljs-built_in'>module</span>.exports = {
  <span class='hljs-attr'>entry</span>: <span class='hljs-string'>'./app/index.js'</span>,
  <span class='hljs-attr'>module</span>: {
    <span class='hljs-attr'>rules</span>: [
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.svg$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'svg-inline-loader'</span> },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.css$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'css-loader'</span> }
    ]
  }
}
</code></pre><br>
Теперь мы можем импортировать как SVG, так и CSS-файлы. Однако для того, чтобы наши стили работали корректно, нужно добавить еще один лоадер. Благодаря <code>css-loader</code> мы можем импортировать CSS-файлы. Но это не означает, что они будут включены в DOM. Мы хотим не только импортировать такие файлы, но и поместить их в тег <code>&lt;style&gt;</code>, чтобы они применялись к элементам DOM. Для этого нужен <code>style-loader</code>. <br>
<br>
<pre><code class='javascript hljs'>npm i style-loader -D 
</code></pre><br>
<pre><code class='javascript hljs'><span class='hljs-built_in'>module</span>.exports = {
  <span class='hljs-attr'>entry</span>: <span class='hljs-string'>'./app/index.js'</span>,
  <span class='hljs-attr'>module</span>: {
    <span class='hljs-attr'>rules</span>: [
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.svg$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'svg-inline-loader'</span> },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.css$/</span>, <span class='hljs-attr'>use</span>: [ <span class='hljs-string'>'style-loader'</span>, <span class='hljs-string'>'css-loader'</span> ] }
    ]
  }
}
</code></pre><br>
Обратите внимание, что поскольку для обработки CSS-файлов используется два лоадера, значением свойства <code>use</code> является массив. Также обратите внимание на порядок следования лоадеров, сначала <code>style-loader</code>, затем <code>css-loader</code>. Это важно. Вебпак будет применять их в обратном порядке. Сначала он использует <code>css-loader</code> для импорта <code>'./styles.css'</code>, затем <code>style-loader</code> для внедрения стилей в DOM. <br>
<br>
Лоадеры могут использоваться не только для импорта файлов, но и для их преобразования. Самым популярным является преобразование JavaScript следующего поколения в современный JavaScript с помощью Babel. Для этого используется <code>babel-loader</code>. <br>
<br>
<pre><code class='javascript hljs'>npm i babel-loader -D 
</code></pre><br>
<pre><code class='javascript hljs'><span class='hljs-built_in'>module</span>.exports = {
  <span class='hljs-attr'>entry</span>: <span class='hljs-string'>'./app/index.js'</span>,
  <span class='hljs-attr'>module</span>: {
    <span class='hljs-attr'>rules</span>: [
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.svg$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'svg-inline-loader'</span> },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.css$/</span>, <span class='hljs-attr'>use</span>: [ <span class='hljs-string'>'style-loader'</span>, <span class='hljs-string'>'css-loader'</span> ] },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.(js)$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'babel-loader'</span> }
    ]
  }
}
</code></pre><br>
Существуют лоадеры почти для любого типа файлов. <br>
<br>
<h3>Точка выхода</h3><br>
Теперь вебпак знает о точке входа и лоадерах. Следующим шагом является указание директории для бандла. Для этого нужно добавить свойство <code>output</code> в настройки вебпака. <br>
<br>
<pre><code class='javascript hljs'><span class='hljs-keyword'>const</span> path = <span class='hljs-built_in'>require</span>(<span class='hljs-string'>'path'</span>)

<span class='hljs-built_in'>module</span>.exports = {
  <span class='hljs-attr'>entry</span>: <span class='hljs-string'>'./app/index.js'</span>,
  <span class='hljs-attr'>module</span>: {
    <span class='hljs-attr'>rules</span>: [
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.svg$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'svg-inline-loader'</span> },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.css$/</span>, <span class='hljs-attr'>use</span>: [ <span class='hljs-string'>'style-loader'</span>, <span class='hljs-string'>'css-loader'</span> ] },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.(js)$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'babel-loader'</span> }
    ]
  },
  <span class='hljs-attr'>output</span>: {
    <span class='hljs-attr'>path</span>: path.resolve(__dirname, <span class='hljs-string'>'dist'</span>),
    <span class='hljs-attr'>filename</span>: <span class='hljs-string'>'index_bundle.js'</span>
  }
}
</code></pre><br>
Весь процесс выглядит примерно так: <br>
<br>
<ol>
<li>Вебпак получает точку входа, находящуюся в <code>./app/index.js</code></li>
<li>Он анализирует операторы <code>import / require</code> и создает граф зависимостей</li>
<li>Вебпак начинает собирать бандл, преобразовывая код с помощью соответствующих лоадеров</li>
<li>Он собирает бандл и помещает его в <code>dist/index_bundle.js</code></li>
</ol><br>
<h3>Плагины (plugins)</h3><br>
Мы рассмотрели, как использовать лоадеры для обработки отдельных файлов перед или в процессе генерации бандла. В отличие от лоадеров, плагины позволяют выполнять задачи после сборки бандла. Эти задачи могут касаться как самого бандла, так и другого кода. Вы можете думать о плагинах как о более мощных, менее ограниченных лоадерах. <br>
<br>
Давайте рассмотрим пример. <br>
<br>
<h3>HtmlWebpackPlugin</h3><br>
Основной задачей вебпака является генерация бандла, на который можно сослаться в <code>index.html</code>. <br>
<br>
<code>HtmlWebpackPlugin</code> создает <code>index.html</code> в директории с бандлом и автоматически добавляет в него ссылку на бандл. <br>
<br>
Мы назвали бандл <code>index_bundle.js</code> и поместили его в <code>dist</code>. <code>HtmlWebpackPlugin</code> создаст новый файл <code>index.html</code> в директории <code>dist</code> и добавит в него ссылку на бандл — <code>&lt;script src='index_bundle.js'&gt;&lt;/script&gt;</code>. Здорово, правда? Поскольку <code>index.html</code> генерируется <code>HtmlWebpackPlugin</code>, даже если мы изменим точку выхода или название бандла, <code>HtmlWebpackPlugin</code> получит эту информацию и изменить содержимое <code>index.html</code>. <br>
<br>
Как нам использовать этот плагин? Как обычно, сначала его нужно установить. <br>
<br>
<pre><code class='javascript hljs'>npm i html-webpack-plugin -D 
</code></pre><br>
Далее добавляем в настройки вебпака свойство <code>plugins</code>. <br>
<br>
<pre><code class='javascript hljs'><span class='hljs-keyword'>const</span> path = <span class='hljs-built_in'>require</span>(<span class='hljs-string'>'path'</span>)

<span class='hljs-built_in'>module</span>.exports = {
  <span class='hljs-attr'>entry</span>: <span class='hljs-string'>'./app/index.js'</span>,
  <span class='hljs-attr'>module</span>: {
    <span class='hljs-attr'>rules</span>: [
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.svg$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'svg-inline-loader'</span> },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.css$/</span>, <span class='hljs-attr'>use</span>: [ <span class='hljs-string'>'style-loader'</span>, <span class='hljs-string'>'css-loader'</span> ] },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.(js)$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'babel-loader'</span> }
    ]
  },
  <span class='hljs-attr'>output</span>: {
    <span class='hljs-attr'>path</span>: path.resolve(__dirname, <span class='hljs-string'>'dist'</span>),
    <span class='hljs-attr'>filename</span>: <span class='hljs-string'>'index_bundle.js'</span>
  },
  <span class='hljs-attr'>plugins</span>: []
}
</code></pre><br>
Создаем экземпляр <code>HtmlWebpackPlugin</code> в массиве <code>plugins</code>. <br>
<br>
<pre><code class='javascript hljs'>
<span class='hljs-keyword'>const</span> path = <span class='hljs-built_in'>require</span>(<span class='hljs-string'>'path'</span>)
<span class='hljs-keyword'>const</span> HtmlWebpackPlugin = <span class='hljs-built_in'>require</span>(<span class='hljs-string'>'html-webpack-plugin'</span>)

<span class='hljs-built_in'>module</span>.exports = {
  <span class='hljs-attr'>entry</span>: <span class='hljs-string'>'./app/index.js'</span>,
  <span class='hljs-attr'>module</span>: {
    <span class='hljs-attr'>rules</span>: [
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.svg$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'svg-inline-loader'</span> },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.css$/</span>, <span class='hljs-attr'>use</span>: [ <span class='hljs-string'>'style-loader'</span>, <span class='hljs-string'>'css-loader'</span> ] },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.(js)$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'babel-loader'</span> }
    ]
  },
  <span class='hljs-attr'>output</span>: {
    <span class='hljs-attr'>path</span>: path.resolve(__dirname, <span class='hljs-string'>'dist'</span>),
    <span class='hljs-attr'>filename</span>: <span class='hljs-string'>'index_bundle.js'</span>
  },
  <span class='hljs-attr'>plugins</span>: [
    <span class='hljs-keyword'>new</span> HtmlWebpackPlugin()
  ]
}
</code></pre><br>
<h3>EnvironmentPlugin</h3><br>
Если вы используете React, то захотите установить <code>process.env.NODE_ENV</code> в значение <code>production</code> перед разворачиванием (деплоем) приложения. Это позволит React осуществить сборку в режиме продакшна, удалив инструменты разработки, такие как предупреждения. Вебпак позволяет это сделать посредством плагина <code>EnvironmentPlugin</code>. Он является частью вебпака, так что его не нужно устанавливать. <br>
<br>
<pre><code class='javascript hljs'><span class='hljs-keyword'>const</span> path = <span class='hljs-built_in'>require</span>(<span class='hljs-string'>'path'</span>)
<span class='hljs-keyword'>const</span> HtmlWebpackPlugin = <span class='hljs-built_in'>require</span>(<span class='hljs-string'>'html-webpack-plugin'</span>)
<span class='hljs-keyword'>const</span> webpack = <span class='hljs-built_in'>require</span>(<span class='hljs-string'>'webpack'</span>)

<span class='hljs-built_in'>module</span>.exports = {
  <span class='hljs-attr'>entry</span>: <span class='hljs-string'>'./app/index.js'</span>,
  <span class='hljs-attr'>module</span>: {
    <span class='hljs-attr'>rules</span>: [
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.svg$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'svg-inline-loader'</span> },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.css$/</span>, <span class='hljs-attr'>use</span>: [ <span class='hljs-string'>'style-loader'</span>, <span class='hljs-string'>'css-loader'</span> ] },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.(js)$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'babel-loader'</span> }
    ]
  },
  <span class='hljs-attr'>output</span>: {
    <span class='hljs-attr'>path</span>: path.resolve(__dirname, <span class='hljs-string'>'dist'</span>),
    <span class='hljs-attr'>filename</span>: <span class='hljs-string'>'index_bundle.js'</span>
  },
  <span class='hljs-attr'>plugins</span>: [
    <span class='hljs-keyword'>new</span> HtmlWebpackPlugin(),
    <span class='hljs-keyword'>new</span> webpack.EnvironmentPlugin({
      <span class='hljs-string'>'NODE_ENV'</span>: <span class='hljs-string'>'production'</span>
    })
  ]
}
</code></pre><br>
Теперь в любом месте нашего приложения мы можем установить режим продакшна с помощью <code>process.env.NODE_ENV</code>. <br>
<br>
<code>HtmlWebpackPlugin</code> и <code>EnvironmentPlugin</code> — это лишь небольшая часть системы плагинов вебпака. <br>
<br>
<h3>Режим (mode)</h3><br>
В процессе подготовки приложения к продакшну, необходимо выполнить несколько действий. Мы только что рассмотрели одно из них — установку <code>process.env.NODE_ENV</code> в значение <code>production</code>. Другое действие заключается в минификации кода и удалении комментариев для уменьшения размера бандла. <br>
<br>
Существуют специальные плагины для решения указанных задачи, но есть более легкий способ. В настройках вебпака можно установить <code>mode</code> в значение <code>development</code> или <code>production</code> в зависимости от среды разработки. <br>
<br>
<pre><code class='javascript hljs'><span class='hljs-keyword'>const</span> path = <span class='hljs-built_in'>require</span>(<span class='hljs-string'>'path'</span>)
<span class='hljs-keyword'>const</span> HtmlWebpackPlugin = <span class='hljs-built_in'>require</span>(<span class='hljs-string'>'html-webpack-plugin'</span>)

<span class='hljs-built_in'>module</span>.exports = {
  <span class='hljs-attr'>entry</span>: <span class='hljs-string'>'./app/index.js'</span>,
  <span class='hljs-attr'>module</span>: {
    <span class='hljs-attr'>rules</span>: [
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.svg$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'svg-inline-loader'</span> },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.css$/</span>, <span class='hljs-attr'>use</span>: [ <span class='hljs-string'>'style-loader'</span>, <span class='hljs-string'>'css-loader'</span> ] },
      { <span class='hljs-attr'>test</span>: <span class='hljs-regexp'>/\.(js)$/</span>, <span class='hljs-attr'>use</span>: <span class='hljs-string'>'babel-loader'</span> }
    ]
  },
  <span class='hljs-attr'>output</span>: {
    <span class='hljs-attr'>path</span>: path.resolve(__dirname, <span class='hljs-string'>'dist'</span>),
    <span class='hljs-attr'>filename</span>: <span class='hljs-string'>'index_bundle.js'</span>
  },
  <span class='hljs-attr'>plugins</span>: [
    <span class='hljs-keyword'>new</span> HtmlWebpackPlugin()
  ],
  <span class='hljs-attr'>mode</span>: <span class='hljs-string'>'production'</span>
}
</code></pre><br>
Обратите внимание, что мы удалили <code>EnvironmentPlugin</code>. Дело в том, что после установки <code>mode</code> в значение <code>production</code> вебпак автоматически присваивает <code>process.env.NODE_ENV</code> значение <code>production</code>. Это также минифицирует код и удаляет предупреждения. <br>
<br>
<h3>Запуск вебпака</h3><br>
На данный момент мы знаем, как работает вебпак и как его настраивать, осталось его запустить. <br>
<br>
У нас есть файл <code>package.json</code>, в котором мы можем создать <code>script</code> для запуска <code>webpack</code>. <br>
<br>
<pre><code class='javascript hljs'><span class='hljs-string'>'scripts'</span>: {
  <span class='hljs-string'>'build'</span>: <span class='hljs-string'>'webpack'</span>
}
</code></pre><br>
Теперь при выполнении команды <code>npm run build</code> в терминале будет запущен вебпак, который создаст оптимизированный бандл <code>index_bundle.js</code> и поместит его в <code>dist</code>. <br>
<br>
<h3>Режимы разработки и продакшна</h3><br>
В целом, мы закончили с вебпаком. Напоследок давайте рассмотрим, как переключаться между режимами. <br>
<br>
При сборке для продакшна, мы хотим все оптимизировать, насколько это возможно. В случае с режимом разработки верно обратное. <br>
<br>
Для переключения между режимами необходимо создать два скрипта в <code>package.json</code>. <br>
<br>
<code>npm run build</code> будет собирать продакшн-бандл. <br>
<br>
<code>npm run start</code> будет запускать сервер для разработки и следить за изменениями файлов. <br>
<br>
Если помните, мы установили <code>mode</code> в значение <code>production</code> в настроках вебпака. Однако теперь нам это не нужно. Мы хотим, чтобы переменная среды имела соответствующее значение в зависимости от выполняемой команды. Немного изменим скрипт <code>build</code> в <code>package.json</code>. <br>
<br>
<pre><code class='javascript hljs'><span class='hljs-string'>'scripts'</span>: {
  <span class='hljs-string'>'build'</span>: <span class='hljs-string'>'NODE_ENV='production' webpack'</span>,
}
</code></pre><br>
Если у вас Windows, то команда будет такой: <code>'SET NODE_ENV='production' &amp;&amp; webpack'</code>. <br>
<br>
Теперь в настроках вебпака мы можем менять значение <code>mode</code> в зависимости от <code>process.env.NODE_ENV</code>. <br>
<br>
<pre><code class='javascript hljs'>...

  mode: process.env.NODE_ENV === <span class='hljs-string'>'production'</span> ? <span class='hljs-string'>'production'</span> : <span class='hljs-string'>'development'</span>
}
</code></pre><br>
Для сборки готового бандла для нашего приложения мы просто запускаем <code>npm run build</code> в терминале. В директории <code>dist</code> создаются файлы <code>index.html</code> и <code>index_bunlde.js</code>. <br>
<br>
<h3>Сервер для разработки</h3><br>
Когда речь идет о разработке приложения принципиально важное значение имеет скорость. Мы не хотим презапускать вебпак и ждать новую сборку при каждом изменении. Вот где нам пригодится пакет <code>webpack-dev-server</code>. <br>
<br>
Как следует из названия, это вебпак-сервер для разработки. Вместо создания дирекории <code>dist</code>, он хранит данные в памяти и обрабатывает их на локальном сервере. Более того, он поддерживает живую перезагрузку. Это означает, что при любом изменении <code>webpack-dev-server</code> пересоберет файлы и перезапустит браузер. <br>
<br>
Устанавливаем пакет.<br>
<br>
<pre><code class='javascript hljs'>npm i webpack-dev-server -D 
</code></pre><br>
Все, что осталось сделать, это добавить скрипт <code>start</code> в <code>package.json</code>. <br>
<br>
<pre><code class='javascript hljs'><span class='hljs-string'>'scripts'</span>: {
  <span class='hljs-string'>'build'</span>: <span class='hljs-string'>'NODE_ENV='production' webpack'</span>,
  <span class='hljs-string'>'start'</span>: <span class='hljs-string'>'webpack-dev-server'</span>
}
</code></pre><br>
Теперь у нас имеется две команды: одна для запуска сервера для разработки, другая для сборки готового бандла. <br>
<br>
Надеюсь, статья была вам полезной. Благодарю за внимание.<p></p>"
                    },
                     new Post()
                    {
                        Id = 3,
                        AccountId = 2,
                        CategoryId = 3,
                        LastChange = DateTime.Now,
                        IsAllowCommenting = false,
                        IsVisibleAll = true,
                        Views = 0,
                        Status = PostStatus.Publish,
                        Title = "Могучие Typescript Декораторы",
                        Anons = @"Typescript Декораторы — как работают, во что компилируются и для каких прикладных задач применимы.",
                        FullText = @"<p>Каждый Ангуляр разработчик видел декораторы в тайпскрипт коде. Их используют, чтобы описать Модули, сконфигурировать Dependency Injection или настроить компонент. Другими словами, декораторы используются, чтобы описать дополнительную информацию, или метаданные, для фреймворка или компилятора (в случае Ангуляра). При чем, Ангуляр лишь один из примеров. Существуют многие другие библиотеки, использующие декораторы для простоты и наглядности кода, как декларативный подход. Как .NET разработчик в прошлом, я вижу много сходства между TS декораторами и .NET аттрибутами. Наконец, набирающий популярность NestJS фреймворк для бекенд приложений (абстракция над Node), также построен на интенсивном использовании декораторов и декларативном подходе. Как это все работает и каким образом использовать декораторы в своем коде, чтобы он был более удобным и читабельным? Мы все понимаем, что после компиляции TS кода мы получаем Javascript код. В котором нет понятия декоратор, как и многих других Typescript особенностей. Поэтому для меня наиболее интересным является вопрос, во что превращается декоратор после компиляции. Занимаясь этим вопросом, я сделал выступление на митапе в Минске и хочу поделиться статьей. </p><hr>
<h1 id='soderzhanie'>Содержание</h1><br>
<ul>
<li>Примеры декораторов</li>
<li>Общая информация о декораторах</li>
<li>Декораторы для функций</li>
<li>Декораторы для классов</li>
<li>Декораторы для полей или свойств класса</li>
<li>Декораторы для параметров — домашняя работа</li>
<li>Существующие библиотеки</li>
<li>Заключение</li>
</ul><hr>
<h1 id='primery-dekoratorov'>Примеры декораторов</h1><br>
<p>Наиболее яркие на мой взгляд примеры работы с декораторами можно найти в Ангуляре. Давайте взглянем на пару из них, перед погружением в технические подробности.</p><br>
<ul>
<li>for Module declaration</li>
</ul><hr>
<pre><code class='javascript hljs'>@NgModule({
  <span class='hljs-attr'>imports</span>: [
    CommonModule,
  ],
  <span class='hljs-attr'>exports</span>: [
  ],
})
<span class='hljs-keyword'>export</span> <span class='hljs-class'><span class='hljs-keyword'>class</span> <span class='hljs-title'>NbThemeModule</span> </span>{}</code></pre><hr>
<ul>
<li>for component declaration</li>
</ul><br>
<pre><code class='javascript hljs'>@Component({
  <span class='hljs-attr'>selector</span>: <span class='hljs-string'>'nb-card-header'</span>,
  <span class='hljs-attr'>template</span>: <span class='hljs-string'>`&lt;ng-content&gt;&lt;/ng-content&gt;`</span>,
})
<span class='hljs-keyword'>export</span> <span class='hljs-class'><span class='hljs-keyword'>class</span> <span class='hljs-title'>NbCardHeaderComponent</span> </span>{}</code></pre><br>
<p>Из примеров видно, что чаще всего декораторы позволяют добавить полезной информации, метаданных, классу. </p><br>
<p>Чтобы начать пользоваться декораторами, стоит проверить <code>tsconfig.json</code> файл, в нем должны быть включены опции <code>emitDecoratorMetadata</code> и <code>experimentalDecorators</code>, так как это все еще <em>экспериментальная</em> функциональность.</p><br>
<pre><code class='javascript hljs'>{
  <span class='hljs-string'>'compilerOptions'</span>: {
    <span class='hljs-string'>'module'</span>: <span class='hljs-string'>'commonjs'</span>,
    <span class='hljs-string'>'declaration'</span>: <span class='hljs-literal'>true</span>,
    <span class='hljs-string'>'removeComments'</span>: <span class='hljs-literal'>true</span>,
    <span class='hljs-string'>'emitDecoratorMetadata'</span>: <span class='hljs-literal'>true</span>,
    <span class='hljs-string'>'experimentalDecorators'</span>: <span class='hljs-literal'>true</span>,
    <span class='hljs-string'>'target'</span>: <span class='hljs-string'>'es2017'</span>,
  },
}</code></pre><br>
<h1 id='obschaya-informaciya-o-dekoratorah'>Общая информация о декораторах</h1><br>
<p>Согласно <a href='https://www.typescriptlang.org/docs/handbook/decorators.html' rel='nofollow'>документации</a>, <em>Декоратор</em> — это специальный вид описания, который можно присоединить к <a href='https://www.typescriptlang.org/docs/handbook/decorators.html#class-decorators' rel='nofollow'>декларации класса</a>, <a href='https://www.typescriptlang.org/docs/handbook/decorators.html#method-decorators' rel='nofollow'>метода</a>, <a href='https://www.typescriptlang.org/docs/handbook/decorators.html#accessor-decorators' rel='nofollow'>get свойства</a>, <a href='https://www.typescriptlang.org/docs/handbook/decorators.html#property-decorators' rel='nofollow'>свойства</a> или <a href='https://www.typescriptlang.org/docs/handbook/decorators.html#parameter-decorators' rel='nofollow'>параметра</a>. Декораторы используют форму <code>@expression</code>, то есть при использовании ставится символ <code>@</code> перед именем декоратора. Хотя по сути <code>expression</code> может быть любая функция. Эта функция будет вызвана в процессе выполнения программы, причем вызывающий код добавит аргументы с информацией о том объекте, который был задекорирован.</p><br>
<p>Другими словами, декоратор — это способ добавить дополнительное поведение классу, функции, свойству или параметру. Это можно отнестик парадигме мета-программирования или декларативного программирования.</p><br>
<p>Важно, что декоратор — это лишь <strong>функция</strong>. При использовании, среда исполнения сначала вызовет функцию-декоратор, и только потом будет выполнен основной сценарий объекта (если код декоратора содержит этот вызов). При наличии нескольких декораторов, они будет вызваны по очереди, сверху вниз.</p><br>
<h1 id='dekoratory-dlya-funkciy'>Декораторы для функций</h1><br>
<p>Начнем с наиболее очевидного случая — декоратора для функции. Определение в самом Typescript выглядит следующим образом:</p><br>
<pre><code class='javascript hljs'>declare type MethodDecorator = 
    &lt;T&gt;(
        target: Object, 
        propertyKey: string | symbol, 
        descriptor: TypedPropertyDescriptor&lt;T&gt;) 
=&gt; TypedPropertyDescriptor&lt;T&gt; | void;</code></pre><br>
<p>Это функция, принимающая несколько аргументов. А именно: </p><br>
<ul>
<li>объект, у которого данная функция была вызвана</li>
<li>имя функции</li>
<li>дескриптор функции</li>
</ul><br>
<p>Дескриптор выглядит так:</p><br>
<pre><code class='javascript hljs'>interface TypedPropertyDescriptor&lt;T&gt; {
    enumerable?: boolean;
    configurable?: boolean;
    writable?: boolean;
    value?: T;
    <span class='hljs-keyword'>get</span>?: () =&gt; T;
    <span class='hljs-keyword'>set</span>?: (value: T) =&gt; void;
}</code></pre><br>
<p>По сути, дескриптор нужен, чтобы получить доступ к исходной функции и иметь возможность ее вызвать из кода декоратора.</p><br>
<p>Стоит отметить, что функция-декоратор будет вызвана не вашим кодом, компилятор сам подставит в нее нужные аргументы. В примере чуть ниже мы посмотрим скомпилированный пример Javascript кода. </p><br>
<p>Чтобы рассмотреть пример, нам понадобится какой-нибудь понятный и полезный сценарий. Например — измерение производительности функции.</p><br>
<pre><code class='javascript hljs'><span class='hljs-class'><span class='hljs-keyword'>class</span> <span class='hljs-title'>TestServiceDeco</span> </span>{

    @LogTime()
        testLogging() {
        ...
    }
}</code></pre><br>
<blockquote>Декоратор для функции, свойства или параметра функции можно применить только внутри некоего класса. В настоящее время компилятор Typescript не позволит применить декоратор для функции, которая написана вне класса. Насколько я понимаю, это связано с необходимостью привязаться к какому-то хранилищу метаданных, необходимо наличие прототипа.</blockquote><p>Для нашего сценария код декоратора может выглядеть таким образом:</p><br>
<pre><code class='javascript hljs'><span class='hljs-function'><span class='hljs-keyword'>function</span> <span class='hljs-title'>LogTime</span>(<span class='hljs-params'></span>) </span>{
    <span class='hljs-keyword'>return</span> <span class='hljs-function'>(<span class='hljs-params'>target: <span class='hljs-built_in'>Object</span>, propertyName: string, descriptor: TypedPropertyDescriptor&lt;<span class='hljs-built_in'>Function</span>&gt;</span>) =&gt;</span> {
        <span class='hljs-keyword'>const</span> method = descriptor.value;
        descriptor.value = <span class='hljs-function'><span class='hljs-keyword'>function</span>(<span class='hljs-params'>...args</span>) </span>{
            <span class='hljs-built_in'>console</span>.time(propertyName || <span class='hljs-string'>'LogTime'</span>);
            <span class='hljs-keyword'>const</span> result = method.apply(<span class='hljs-keyword'>this</span>, args);
            <span class='hljs-built_in'>console</span>.timeEnd(propertyName || <span class='hljs-string'>'LogTime'</span>);
            <span class='hljs-keyword'>return</span> result;
        };
    };
}</code></pre><br>
<p>Как я сказал ранее, декоратор — это функция, которая возвращает функцию определенного типа. В примере видны аргументы этой функции — target, propertyName и дескриптор функции. Их компилятор подставит в вызывающий код. </p><br>
<p>Дескриптор функции здесь позволяет переопределить поведение — подменить искомую функцию на новую, которая уже следует заданной декоратором логике. Наша логика подразумевает засечь момент старта функции, и ее завершения, и вывести разницу в консоль. Конечно не стоит забывать вернуть значение искомой функции.</p><br>
<p>Скомпилированнй Javascript код будет выглядеть следующим образом</p><br>
<pre><code class='javascript hljs'><span class='hljs-meta'>'use strict'</span>;
<span class='hljs-built_in'>Object</span>.defineProperty(exports, <span class='hljs-string'>'__esModule'</span>, { <span class='hljs-attr'>value</span>: <span class='hljs-literal'>true</span> });
<span class='hljs-function'><span class='hljs-keyword'>function</span> <span class='hljs-title'>LogTime</span>(<span class='hljs-params'></span>) </span>{
    <span class='hljs-keyword'>return</span> <span class='hljs-function'>(<span class='hljs-params'>target, propertyName, descriptor</span>) =&gt;</span> {
        <span class='hljs-keyword'>const</span> method = descriptor.value;
        descriptor.value = <span class='hljs-function'><span class='hljs-keyword'>function</span> (<span class='hljs-params'>...args</span>) </span>{
            <span class='hljs-built_in'>console</span>.time(propertyName || <span class='hljs-string'>'LogTime'</span>);
            <span class='hljs-keyword'>const</span> result = method.apply(<span class='hljs-keyword'>this</span>, args);
            <span class='hljs-built_in'>console</span>.timeEnd(propertyName || <span class='hljs-string'>'LogTime'</span>);
            <span class='hljs-keyword'>return</span> result;
        };
    };
}
exports.LogTime = LogTime;</code></pre><br>
<p>Тут никаких сюрпризов, все примерно как и в Typescript коде. А вот код вызывающий уже интереснее:</p><br>
<pre><code class='javascript hljs'><span class='hljs-built_in'>Object</span>.defineProperty(exports, <span class='hljs-string'>'__esModule'</span>, { <span class='hljs-attr'>value</span>: <span class='hljs-literal'>true</span> });
<span class='hljs-keyword'>const</span> log_time_decorator_1 = <span class='hljs-built_in'>require</span>(<span class='hljs-string'>'../src/samples/log-time.decorator'</span>);
<span class='hljs-class'><span class='hljs-keyword'>class</span> <span class='hljs-title'>TestServiceDeco</span> </span>{
    testLogging() {
...    }
}
__decorate([
    log_time_decorator_1.LogTime(),
    __metadata(<span class='hljs-string'>'design:type'</span>, <span class='hljs-built_in'>Function</span>),
    __metadata(<span class='hljs-string'>'design:paramtypes'</span>, []),
    __metadata(<span class='hljs-string'>'design:returntype'</span>, <span class='hljs-keyword'>void</span> <span class='hljs-number'>0</span>)
], TestServiceDeco.prototype, <span class='hljs-string'>'testLogging'</span>, <span class='hljs-literal'>null</span>);</code></pre><br>
<p>Тут уже видна системная функция <code>__decorate</code>, в которую передается наш декоратор вместе с дополнительными аргументами. </p><br>
<blockquote>Заметим, что в качестве <code>target</code> аргумент подставлен <code>prototype</code> класса, в котором определена функция.</blockquote><p>Подставленный компилятором код, вызывающий функцию <code>__decorate</code>, будет выполнен в процессе интерпретации кода, сразу после интерпретации класса. Но сам код нашего декоратора будет вызываться каждый раз, когда вызывается исходная функция. Это ключевое отличие от следующего вида декораторов.</p><br>
<h1 id='dekoratory-dlya-klassov'>Декораторы для классов</h1><br>
<p>Этот вид декораторов как правило используется, чтобы добавить классу метаданных. Где и как они будут использованы — уже другой вопрос. В Ангуляре — это подсказки компилятору. Но есть и более понятные сценарии — например Dependency Injection. Давайте напишем свой простой и легкий контейнер зависимостей на основе декоратора класса. Например, мы бы могли его использовать следующим образом:</p><br>
<pre><code class='javascript hljs'>@CustomBehavior({
    <span class='hljs-attr'>singleton</span>: <span class='hljs-literal'>false</span>,
})
<span class='hljs-class'><span class='hljs-keyword'>class</span> <span class='hljs-title'>TestServiceDeco</span> </span>{
    <span class='hljs-keyword'>constructor</span>() {
        <span class='hljs-built_in'>console</span>.log(<span class='hljs-string'>'TestServiceDeco ctor'</span>);
    }
}</code></pre><br>
<p>Прежде чем приступить, посмотрим формальное определение декоратора класса в Typescript:</p><br>
<pre><code class='javascript hljs'>declare type ClassDecorator = 
    <span class='xml'><span class='hljs-tag'>&lt;<span class='hljs-name'>TFunction</span> <span class='hljs-attr'>extends</span> <span class='hljs-attr'>Function</span>&gt;</span>(target: TFunction) 
    =&gt; TFunction | void;</span></code></pre><br>
<p>Таким образом наш декоратор будет выглядить таким образом:</p><br>
<pre><code class='javascript hljs'><span class='hljs-keyword'>import</span> <span class='hljs-string'>'reflect-metadata'</span>;

interface Metadata {
    singleton?: boolean;
}

<span class='hljs-function'><span class='hljs-keyword'>function</span> <span class='hljs-title'>CustomBehavior</span>(<span class='hljs-params'>metadata: Metadata</span>) </span>{
    <span class='hljs-keyword'>return</span> <span class='hljs-function'><span class='hljs-keyword'>function</span>(<span class='hljs-params'>ctor: Function</span>) </span>{
        <span class='hljs-built_in'>Reflect</span>.defineMetadata(<span class='hljs-string'>'metadataKey'</span>, metadata, ctor);
    }
}</code></pre><br>
<p>Мы определили интерфейс для придания структуры нашим метаданным. Самая важная для нас информация — является ли данный класс singelton-ом или же его можно инстанциировать многократно. Дальше мы просто сохраняем данную информацию для дальнейшего использования. </p><br>
<p>Пара важных моментов:</p><br>
<ul>
<li>сейчас в качестве target аргумента мы ожидаем конструктор класса</li>
<li>мы начали использовать <code>reflect-metadata</code></li>
</ul><br>
<p>Reflect-metadata это хранилище метаданных в Typescript. Его смысл тот же, что и в других языках — хранить информацию о типах для работы с ней в процессе выполнения программы. В нашем случае, мы добавили свои метаданные класса, которыми будем пользоваться в своем контейнере зависимостей.</p><br>
<pre><code class='javascript hljs'><span class='hljs-keyword'>import</span> <span class='hljs-string'>'reflect-metadata'</span>;

<span class='hljs-keyword'>const</span> instancesMap: <span class='hljs-built_in'>Map</span>&lt;<span class='hljs-built_in'>Object</span>, <span class='hljs-built_in'>Object</span>&gt; = <span class='hljs-keyword'>new</span> <span class='hljs-built_in'>Map</span>&lt;<span class='hljs-built_in'>Object</span>, <span class='hljs-built_in'>Object</span>&gt;();

<span class='hljs-function'><span class='hljs-keyword'>function</span> <span class='hljs-title'>getInstance</span>&lt;<span class='hljs-title'>T</span>&gt;(<span class='hljs-params'>tType: new (</span>) =&gt; <span class='hljs-title'>T</span>): <span class='hljs-title'>T</span> </span>{
    <span class='hljs-keyword'>let</span> metadata = <span class='hljs-built_in'>Reflect</span>.getMetadata(<span class='hljs-string'>'metadataKey'</span>, tType) <span class='hljs-keyword'>as</span> Metadata;
    <span class='hljs-keyword'>if</span> (metadata.singleton) {
        <span class='hljs-keyword'>if</span> (!instancesMap.has(tType)) {
            instancesMap.set(tType, <span class='hljs-keyword'>new</span> tType());
        }
        <span class='hljs-keyword'>return</span> instancesMap.get(tType) <span class='hljs-keyword'>as</span> T;
    } <span class='hljs-keyword'>else</span> {
        <span class='hljs-keyword'>return</span> <span class='hljs-keyword'>new</span> tType() <span class='hljs-keyword'>as</span> T;
    }
}</code></pre><br>
<ul>
<li>наш контейнер состоит из единственной функции <code>getInstance</code>, в которую будет передаваться тип, класс, экземпляр которого необходимо создать</li>
<li>с помощью <code>Reflect.getMetadata</code> мы получаем информацию, которую передали с помощью декоратора. Так как эта функция возвращает <code>any</code>, нам приходится добавлять <code>as Metadata</code> для приведения к своему типу</li>
<li>так как нам необходимо создавать экземпляры, нам нужен конструктор. Поэтому накладываем ограничение <code>tType: new () =&gt; T</code></li>
<li>и конечно нужен какой-то способ хранения созданных экземпляров, в нашем простом случае это Map</li>
</ul><br>
<p>Вуаля, в несколько сравнительно простых строк кода мы написали свой контейнер зависимостей и декоратор для определения, может ли класс иметь несколько экземпляров или только один.</p><br>
<p>Теперь можем где угодно в коде вызывать <code>getInstance</code>, а как может быть создан класс уже прописано в его декораторе.</p><br>
<blockquote>Я не стал приводить скомпилированный код данного декоратора и класса, он не сильно отличается от прошлого примера. Но ключевым моментом является тот факт, что код декоратора класса будет выполнен только один раз при интерпретации Javascript кода этого файла. </blockquote><br>
<h1 id='dekoratory-dlya-poley-ili-svoystv-klassa'>Декораторы для полей или свойств класса</h1><br>
<p>Еще одна область применения декораторов относится к свойствам класса. Тут открывается целый спектр прикладных задач, но наиболее насущной, на мой взгляд, является валидация данных. Представьте, есть класс <code>Person</code> с полем <code>Age</code>, значения которого по логике приложения должно быть между 18 и 60. Давайте сделаем данную проверку с помощью декоратора:</p><br>
<pre><code class='javascript hljs'><span class='hljs-class'><span class='hljs-keyword'>class</span> <span class='hljs-title'>Person</span> </span>{
    @Age(<span class='hljs-number'>18</span>, <span class='hljs-number'>60</span>)
    <span class='hljs-attr'>age</span>: number;
}</code></pre><br>
<p>Снова обратимся к формальному определению:</p><br>
<pre><code class='javascript hljs'>declare type PropertyDecorator = 
    <span class='hljs-function'>(<span class='hljs-params'>target: <span class='hljs-built_in'>Object</span>, propertyKey: string | symbol</span>) =&gt;</span> <span class='hljs-keyword'>void</span>;</code></pre><br>
<p>Наш декоратор для валидации выглядит следующим образом:</p><br>
<pre><code class='javascript hljs'><span class='hljs-keyword'>import</span> <span class='hljs-string'>'reflect-metadata'</span>;

<span class='hljs-function'><span class='hljs-keyword'>function</span> <span class='hljs-title'>Age</span>(<span class='hljs-params'>from: number, to: number</span>) </span>{
    <span class='hljs-keyword'>return</span> <span class='hljs-function'><span class='hljs-keyword'>function</span> (<span class='hljs-params'>object: Object, propertyName: string</span>) </span>{
        <span class='hljs-keyword'>const</span> metadata = {
            <span class='hljs-attr'>propertyName</span>: propertyName,
            <span class='hljs-attr'>range</span>: { <span class='hljs-keyword'>from</span>, to },
        };
        <span class='hljs-built_in'>Reflect</span>.defineMetadata(<span class='hljs-string'>`validationMetadata_<span class='hljs-subst'>${propertyName}</span>`</span>, metadata, object.constructor);
    };
}</code></pre><br>
<p>И снова мы видим, что основной логики тут нет. Мы просто сохраняем нужную нам информацию в хранилище метаданных. Все потому, что это код, как и код декоратора класса, будет выполнен только один раз при прочтении кода. До того, как конструктор класса был вызван.</p><br>
<p>Скомпилированный код:</p><br>
<pre><code class='javascript hljs'><span class='hljs-class'><span class='hljs-keyword'>class</span> <span class='hljs-title'>Person</span> </span>{
...
}
__decorate([
    age_decorator_1.Age(<span class='hljs-number'>18</span>, <span class='hljs-number'>60</span>),
    __metadata(<span class='hljs-string'>'design:type'</span>, <span class='hljs-built_in'>Number</span>)
], Person.prototype, <span class='hljs-string'>'age'</span>, <span class='hljs-keyword'>void</span> <span class='hljs-number'>0</span>);</code></pre><br>
<p>Видим, что сразу после определения класса компилятор поместил свою функцию <code>__decorate</code>, в которую передал наш декоратор с параметрами.</p><br>
<p>Это своеобразное подтверждение того, что основная задача декораторов — сделать код более удобным к прочтению, информативно богатым. В случае валидации — описать правила проверок в том же месте, где и сам класс, причем в удобной форме.</p><br>
<p>Возвращаясь к валидации, ее необходимо описать отдельно:</p><br>
<pre><code class='javascript hljs'><span class='hljs-function'><span class='hljs-keyword'>function</span> <span class='hljs-title'>validate</span>&lt;<span class='hljs-title'>T</span>&gt;(<span class='hljs-params'>object: T</span>) </span>{
    <span class='hljs-keyword'>const</span> properties = <span class='hljs-built_in'>Object</span>.getOwnPropertyNames(object);
    properties.forEach(<span class='hljs-function'><span class='hljs-params'>propertyName</span> =&gt;</span> {
        <span class='hljs-keyword'>let</span> metadata = <span class='hljs-built_in'>Reflect</span>.getMetadata(metaKey + propertyName, object.constructor);
        <span class='hljs-keyword'>if</span> (metadata &amp;&amp; metadata.range) {
            <span class='hljs-keyword'>const</span> value = object[metadata.propertyName];
            <span class='hljs-keyword'>if</span> (value &lt; metadata.range.from || value &gt; metadata.range.to) {
                <span class='hljs-keyword'>throw</span> <span class='hljs-keyword'>new</span> <span class='hljs-built_in'>Error</span>(<span class='hljs-string'>'Validation failed'</span>);
            }
        }
    });
}</code></pre><br>
<p>В примере, конечно же, мы делаем одну единственную проверку. Реальный сценарий будет несколько сложнее.</p><br>
<p>Пример вызова:</p><br>
<pre><code class='javascript hljs'><span class='hljs-keyword'>const</span> person = <span class='hljs-keyword'>new</span> Person();
person.age = <span class='hljs-number'>40</span>;
validate(person);
<span class='hljs-comment'>// &gt; validation passed</span>

person.age = <span class='hljs-number'>16</span>;
validate(person);
<span class='hljs-comment'>// &gt; validation error</span></code></pre><br>
<h1 id='dekoratory-dlya-parametrov-funkciy'>Декораторы для параметров функций</h1><br>
<p>Если вы дочитали до этого места, я уверен, что вы получили достаточно информации и понимания, чтобы сделать маленькую домашнюю работу и разобраться в декораторах для параметров функций самостоятельно.</p><br>
<p>Формально же, декоратор для параметра функции выглядит таким образом:</p><br>
<pre><code class='javascript hljs'>declare type ParameterDecorator = <span class='hljs-function'>(<span class='hljs-params'>target: <span class='hljs-built_in'>Object</span>, propertyKey: string | symbol, parameterIndex: number</span>) =&gt;</span> <span class='hljs-keyword'>void</span>;</code></pre><br>
<h1 id='suschestvuyuschie-biblioteki'>Существующие библиотеки</h1><br>
<h2 id='class-validator'>Class-Validator</h2><br>
<p>Я бы хотел привести библиотеку Class-Validator, использование которой для меня лично очень удобно. Ее декораторы постоянно используются в коде моих проектов.</p><br>
<pre><code class='javascript hljs'><span class='hljs-keyword'>export</span> <span class='hljs-class'><span class='hljs-keyword'>class</span> <span class='hljs-title'>Post</span> </span>{

    @Length(<span class='hljs-number'>10</span>, <span class='hljs-number'>20</span>)
    <span class='hljs-attr'>title</span>: string;

    @IsInt()
    @Min(<span class='hljs-number'>0</span>)
    @Max(<span class='hljs-number'>10</span>)
    <span class='hljs-attr'>rating</span>: number;

    @IsEmail()
    <span class='hljs-attr'>email</span>: string;
}

...

validate(object).then(<span class='hljs-function'><span class='hljs-params'>errors</span> =&gt;</span> { <span class='hljs-comment'>// array of validation errors</span>
    <span class='hljs-keyword'>if</span> (errors.length &gt; <span class='hljs-number'>0</span>) {
        <span class='hljs-built_in'>console</span>.log(<span class='hljs-string'>'validation failed. errors: '</span>, errors);
    } <span class='hljs-keyword'>else</span> {
        <span class='hljs-built_in'>console</span>.log(<span class='hljs-string'>'validation succeed'</span>);
    }
});</code></pre><br>
<p>Думаю, из примера все понятно. Прочие же детали можно найти в <a href='https://github.com/typestack/class-validator' rel='nofollow'>репозитории</a>.</p><br>
<p>Интересный факт в том, что именно эта библиотека используется по умолчанию в фреймворке <a href='https://nestjs.com/' rel='nofollow'>NestJS</a> когда применяется <code>@UsePipes(new ValidationPipe())</code> для валидации всех входящих http запросов.</p><br>
<h1 id='zaklyuchenie'>Заключение</h1><br>
<p>Потенциал Typescript по созданию удобного, простого для прочтения и надежного кода очень велик. Его можно использовать сразу в нескольких парадигмах, в том числе для мета-программирования. Декораторы, даже будучи экспериментальной функциональностью, дают возможности для решения целого спектра прикладных задач, помогают сделать код простым для прочтения и более удобным для работы, помогают в решении таких задач как логирование, измерение производительности, проверки, дополнительное поведение… Чем и пользуются такие фреймворки как Angular и NestJS. Понимание декораторов помогает писать код более красиво (пусть это и субьективная оценка).</p><br>
<p>Пишите код, улучшайте его, вычишайте его, делайте его красивым, тестируйте и наконец наслаждайтесь как проделанной работой, так и самим процессом создания кода!</p><br>
<p>p.s. пока писал (вернее переводил свою же статью), нашел еще одну тут же на хабре, которая хорошо раскрывает тему декораторов. Привожу <a href='https://habr.com/ru/company/ivi/blog/275003'>ссылку</a></p><p></p>"
                    },
                     new Post()
                    {
                        Id = 4,
                        AccountId = 2,
                        CategoryId = 3,
                        LastChange = DateTime.Now,
                        IsAllowCommenting = false,
                        IsVisibleAll = true,
                        Views = 0,
                        Status = PostStatus.Publish,
                        Title = "Spring Boot 3.0 — готовимся заранее",
                        Anons = @"JetBrains выпустила IDEA с поддержка Spring 6 и Spring Boot 3.",
                        FullText = @"<p><p>Здравствуй, читатель Хабра!</p><p>До выхода Spring Boot 3 осталось совсем немного - 3 месяца. Уже появляются статьи -<br><a href=""https://www.baeldung.com/spring-boot-3-spring-6-new"" rel=""noopener noreferrer nofollow"">What’s New</a>, <a href=""https://www.springcloud.io/post/2022-05/springboot-3-0"" rel=""noopener noreferrer nofollow"">It's time to get ready</a>. Недавно JetBrains выпустила IDEA <a href=""https://www.jetbrains.com/ru-ru/idea/whatsnew/"" rel=""noopener noreferrer nofollow"">с поддержка Spring 6 и Spring Boot 3</a>. Самое время потренироваться заранее в миграции. В разработке нового учебного курса я попробовал перевести свой открытый учебный проект Spring Boot 2.x + HATEOAS  на Spring Boot 3, шаги и код проекта ниже.</p><p>За основу взят код открытого проекта  Spring Boot 2.x + HATEOAS (<a href=""https://github.com/JavaOPs/bootjava/tree/patched"" rel=""noopener noreferrer nofollow"">код на GitHub в ветке patched</a>). Функционал простой - основа любого современного REST веб-приложения: аутентификация и авторизация на основе ролей, регистрация пользователя в приложении, управление своим профилем и администрирование пользователей.  </p><p><a href=""https://github.com/JavaOPs/cloudjava/commits/patched"" rel=""noopener noreferrer nofollow"">Первым комитом (ветка patched)</a> перевел проект с Maven на Gradle - давно хотел, появился повод:) Примечание - против Maven ничего не имею, для сравнения Gradle и Maven есть отдельные статьи и дискуссии.</p><p>Далее будет разбор второго шага - кода миграции: <a href=""https://github.com/JavaOPs/cloudjava/commit/2d74f6158b8380587a3360e911e3a6ff42c49642"" rel=""noopener noreferrer nofollow"">сommit details</a><br>Для разбора проще всего вычекать к себе проект с этой ревизией:<br><br><code>git clone --branch patched https://github.com/JavaOPs/cloudjava<br>cd cloudjava<br>git checkout 2d74f6158b8380587a3360e911e3a6ff42c49642</code></p><ul><li><p>Обновляем версию Spring и добавляем&nbsp;snapshot репозитории в&nbsp;<code>build.gradle</code></p></li><li><p>Меняем зависимость&nbsp;<code>springdoc-openapi</code>&nbsp;на <code>springdoc-openapi-starter-webmvc-ui</code>  и пакет&nbsp;для <code>GroupedOpenApi</code>:&nbsp;смотри <a href=""https://github.com/springdoc/springdoc-openapi-demos/wiki/springdoc-openapi-2.x-migration-guide"" rel=""noopener noreferrer nofollow"">SpringDoc OpenAPI 2.x migration guide</a></p></li><li><p>Добавляем&nbsp;snapshot репозитории в&nbsp;<code>settings.gradle</code></p></li><li><p>В коде всего проекта меняем <code>javax.validation</code>&nbsp; и &nbsp;<code>javax.servlet</code>&nbsp; на&nbsp; <code>jakarta</code> (можно контекстной заменой). Здравствуй JPA 3, Hibernate 6, Hibernate Validator 7 и Tomcat 10 !</p></li><li><p>Обновляем зависимость <code>jackson-datatype-hibernate5</code>&nbsp;на&nbsp;<code>jackson-datatype-hibernate5-jakarta</code>. В&nbsp;<code>AppConfig</code><a href=""https://github.com/JavaOPs/cloudjava/commit/3be427123b4efd9e97c000187b8970b3c4be6e80#diff-465dabf1f99ce71c88fe107bc42a3598b1a6c2056929bb8c615035d898b045b7"" rel=""noopener noreferrer nofollow"">&nbsp;</a>также делаем замену &nbsp;<code>Hibernate5Module</code>&nbsp; на&nbsp; <code>Hibernate5JakartaModule</code></p></li><li><p>В&nbsp;<code>GlobalExceptionHandler</code><a href=""https://github.com/JavaOPs/cloudjava/commit/3be427123b4efd9e97c000187b8970b3c4be6e80#diff-0bad4681446ba06bce931242ff82be933101008537a35c66c4048a7b41925233"" rel=""noopener noreferrer nofollow"">&nbsp;</a>меняем&nbsp;<code>HttpStatus</code>&nbsp;на&nbsp;<code>HttpStatusCode</code>. Появилась новая иерархия статусов возврата без требования быть <code>enum</code>. Однако для <code>getReasonPhrase()</code> теперь приходится делать <code>instanceof HttpStatus</code></p></li><li><p>В&nbsp;<code>AdminUserControllerTest</code>&nbsp;не идут тесты на запросы со слешем в конце. Сделал отдельную переменную&nbsp;<code>REST_URL_SLASH</code></p></li></ul><p>Проект совсем небольшой, поэтому, если у вас встретятся неописанные здесь шаги, пишите в комментариях. Также буду рад любым замечаниям по коду.<br><br>И - желаю успехов с обновлением на Spring Boot 3!</p><p></p></p>"
                    },

                });
        }
    }
}
