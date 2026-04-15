# Individual Journal - Member 4

## Name and Role in the Group
**Name:** [Name]
**Role:** UI/UX & Integration Specialist

## Specific Tasks Performed
My role concentrated on providing visual professionalism, intuitive user experience (UX), and seamless visual integration across both the Web Client and the desktop C# Application. Because our team built two separate end-user platforms with different foundational technologies (HTML vs. WinForms), my overarching task was to bridge the aesthetic gap. I ensured that despite operating in different application frameworks, both clients felt visually aligned, accessible, and satisfying for the user to navigate. 

## Code Contributions
In the web environment, I was the sole author of `style.css`. Instead of defaulting to heavy UI frameworks, I custom-coded a sleek, modern visual aesthetic. I accomplished this heavily utilizing CSS Custom Properties (variables) for consistent branding pallets, as well as structurally deploying modern CSS Flexbox parameters to automatically center items on the screen flawlessly. For interactivity elements, I installed transitions utilizing `:hover` modifiers providing tactile visual feedback to user cursors. On the C# side, while I didn't write network code, I systematically overhauled the Form properties. I customized the `Anchor` and `Dock` parameters allowing the desktop windows to resize fluidly, adjusting font structures to mimic modern sans-serifs, and refining control padding parameters.

## Challenges Encountered
Creating authentic "responsive design" purely from scratch proved remarkably difficult. Ensuring that data tables populated by the REST API didn't warp or explode past the screen resolution boundaries when accessed by small laptop screens required intense debugging. Additionally, in the Windows Forms context, attempting to inject aesthetic padding around raw `DataGridView` constraints consistently produced rigid, outdated-looking rectangles that contrasted horribly with my web-client's more modern design.

## How they Contributed to Solving Problems
I tackled the CSS responsiveness issue by installing distinct Media Queries within my stylesheet. I created break point triggers that automatically stacked the input forms on top of the data tables on reduced resolutions, entirely preserving the fluid interaction. In the C# constraints, I managed to polish the aesthetic by entirely disabling the default rigid row headers and tweaking the cell-border colorations to soft grayscale palettes. Ultimately, my efforts in directly tackling the aesthetic limitations on both platforms successfully transformed our functional codebase into a truly premium, modern-looking final product ready for submission.
